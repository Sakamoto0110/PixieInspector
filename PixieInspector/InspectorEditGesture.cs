using System.Globalization;

namespace PixieInspector;

// A flat snapshot of each member and each bound object's displayed value, never recursive setters.
public sealed class InspectorEditGesture : IDisposable
{
    private sealed record Start(InspectorNode Node, object?[] Values, bool Pending, long Version, object[] Instances);
    private readonly InspectorNode Source;
    private readonly Start[] Starts;
    private bool Finished;
    public InspectorEditResult? LastResult { get; private set; }

    internal InspectorEditGesture(InspectorNode source)
    {
        source.CheckDirectEdit();
        Source = source;
        var members = new[] { source }.Concat(source.ChainedWith.Where(node => node != source));
        Starts = members.Select(node => new Start(node, node.ViewValues.ToArray(), node.HasPendingValue,
            node.EditVersion, node.Inspector.Instances.ToArray())).ToArray();
    }

    public InspectorNode Node => Source;
    public IReadOnlyList<InspectorNode> Members => Array.AsReadOnly(Starts.Select(s => s.Node).ToArray());

    public InspectorEditResult SetValue(object? value) => Run(start => start.Node.SetValue(value));

    // Total delta from the gesture's start, in displayed units. Decimal arithmetic retains integer precision.
    public InspectorEditResult SetDelta(double delta)
    {
        if (!double.IsFinite(delta)) throw new ArgumentOutOfRangeException(nameof(delta));
        return Run(start => start.Node.SetValues(start.Values.Select(value => Add(value, delta)).ToArray()));
    }

    public InspectorEditResult Cancel()
    {
        var result = Run(start =>
        {
            start.Node.SetValues(start.Values);
            start.Node.RestoreEditPending(start.Pending);
        }, cancel: true);
        Finished = true;
        return result;
    }

    private InspectorEditResult Run(Action<Start> write, bool cancel = false)
    {
        ObjectDisposedException.ThrowIf(Finished, this);
        if (!cancel) Source.CheckDirectEdit();
        // Reject the complete stale gesture before any target is written, including equal-count rebinds.
        if (Starts.Any(s => s.Node.EditDisposed || s.Node.EditVersion != s.Version ||
            !s.Node.Inspector.Instances.SequenceEqual(s.Instances, ReferenceEqualityComparer.Instance)))
            throw new InvalidOperationException("The edit gesture is stale; start a new interaction.");
        var outcomes = new List<InspectorEditOutcome>();
        foreach (var start in Starts)
        {
            Exception? error = null;
            try
            {
                if (Starts.Any(s => s.Node.EditDisposed || s.Node.EditVersion != s.Version ||
                    !s.Node.Inspector.Instances.SequenceEqual(s.Instances, ReferenceEqualityComparer.Instance)))
                    throw new InvalidOperationException("A target changed during the gesture; remaining writes were refused.");
                write(start);
            }
            catch (Exception e) { error = e; }
            outcomes.Add(new(start.Node, start.Node.HasPendingValue, start.Node.Failure, error));
        }
        return LastResult = new InspectorEditResult(outcomes);
    }

    private static object Add(object? value, double delta) => value switch
    {
        double n => n + delta,
        float n => (double)n + delta,
        null => throw new InvalidOperationException("Cannot scrub a missing or unreadable value."),
        _ when InspectorNode.IsEditNumber(value.GetType()) => Convert.ToDecimal(value, CultureInfo.InvariantCulture) + (decimal)delta,
        _ => throw new InvalidOperationException("Only numeric values support delta edits."),
    };

    public void Dispose() => Finished = true;
}
