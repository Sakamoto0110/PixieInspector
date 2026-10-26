using PixieInspector.Diagnostics;
using PixieInspector.Model;
using PixieInspector.Options;

namespace PixieInspector;

public abstract partial class InspectorNode
{
    private InspectorFailureEventArgs? FlagChoiceFailure;

    // Read when the view opens its popup. Setting it selects Flags; clearing it restores enum
    // choices, without overwriting a manually selected editor or another option layer.
    public Func<IEnumerable<FlagChoice>>? FlagChoices
    {
        get;
        set
        {
            Change(ref field, value);
            if (value != null)
                Editor = EditorKind.Flags;
        }
    }

    public IReadOnlyList<FlagChoice> GetFlags()
    {
        var type = FlagType();
        try
        {
            var choices = FlagChoices is { } read ? read()?.ToArray() ?? []
                : type.IsEnum ? Enum.GetNames(type).Select(name => new FlagChoice(
                    FlagBits.Read(Enum.Parse(type, name), type), name)).ToArray() : [];
            foreach (var choice in choices)
            {
                ArgumentNullException.ThrowIfNull(choice);
                ArgumentNullException.ThrowIfNull(choice.Label);
                CheckMask(choice.Mask, type);
            }
            // Retrying a recovered provider clears only its own diagnostic. A failed value read
            // or a rejected write still belongs to the normal binder and must not be hidden.
            if (Failure != null && ReferenceEquals(Failure, FlagChoiceFailure))
                OnWritten();
            FlagChoiceFailure = null;
            return choices;
        }
        catch (Exception e)
        {
            var previous = Failure;
            var previousWasWrite = FailedOnWrite;
            try
            {
                OnBindFailed(FailureSeverity.Recovered, Unwrap(e), $"The flags of '{Path}' could not be read.",
                    "Check FlagChoices and the member's storage width; the popup retries when opened.", onWrite: false);
            }
            finally
            {
                if (previous != null && !ReferenceEquals(previous, FlagChoiceFailure))
                {
                    Failure = previous;
                    FailedOnWrite = previousWasWrite;
                }
                else FlagChoiceFailure = Failure;
            }
            return [];
        }
    }

    // Null means mixed, a partial composite, a nullable value with no bits yet, or no binding.
    // This reads the held values, never getters, so it respects InstanceToView and pending edits.
    public bool? GetFlagState(ulong mask)
    {
        var type = FlagType();
        CheckMask(mask, type);
        var values = ViewValues;
        if (ReadFailed || values.Count == 0)
            return null;
        bool? result = null;
        for (var i = 0; i < values.Count; ++i)
        {
            if (values[i] is not { } value)
                return null;
            var bits = FlagBits.Read(value, type);
            bool? state = mask == 0 ? bits == 0 : (bits & mask) == mask ? true : (bits & mask) == 0 ? false : null;
            if (i > 0 && result != state)
                return null;
            result = state;
        }
        return result;
    }

    // Edits only this mask in each object's own held value, retaining unrelated/unknown bits.
    // Explicitly selecting zero clears the entire value; deselecting zero changes nothing.
    // SetValues retains normal rules, read-only guards, pending values and write notifications.
    public void SetFlag(ulong mask, bool enabled)
    {
        var type = FlagType();
        CheckMask(mask, type);
        ThrowIfCompromised();
        if (ReadFailed)
            throw new InvalidOperationException($"Cannot edit flags of '{Path}' after a failed read.");
        if (mask == 0 && !enabled)
            return;
        var values = ViewValues.Select(value =>
        {
            var bits = value == null ? 0 : FlagBits.Read(value, type);
            return (object?)FlagBits.Write(mask == 0 ? 0 : enabled ? bits | mask : bits & ~mask, type);
        }).ToArray();
        SetValues(values);
    }

    private Type FlagType()
    {
        var type = ValueType ?? throw new InvalidOperationException("Flags require a typed member.");
        type = Nullable.GetUnderlyingType(type) ?? type;
        _ = FlagBits.WidthMask(type);
        if (Unit != null && !type.IsEnum)
            throw new InvalidOperationException("An integral flag mask cannot use a display unit.");
        return type;
    }

    private static void CheckMask(ulong mask, Type type)
    {
        if ((mask & ~FlagBits.WidthMask(type)) != 0)
            throw new ArgumentOutOfRangeException(nameof(mask), "The flag mask exceeds the member's storage width.");
    }
}
