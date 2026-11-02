using PixieInspector.Model;

namespace PixieInspector;

public abstract partial class InspectorNode
{
    private readonly HashSet<InspectorNode> ChainPeers = [];
    private IReadOnlyList<InspectorNode>? Chain;
    private IReadOnlyList<InspectorNode> ChainPeerView = Array.Empty<InspectorNode>();
    internal long EditVersion { get; private set; }
    internal bool EditDisposed { get; private set; }

    // A UI lock, deliberately independent of ReadOnly and programmatic setters.
    public bool IsEditLocked
    {
        get;
        set => Change(ref field, value);
    }

    public bool SupportsEditControls => this is MemberNode and not CollectionNode && !IsGroup;
    public bool IsChainEnabled => Chain != null;

    // Setting either endpoint replaces its edges and normalizes the reverse edges too.
    public IReadOnlyList<InspectorNode> ChainsWith
    {
        get => ChainPeerView;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            ObjectDisposedException.ThrowIf(EditDisposed, this);
            var peers = value.Distinct().ToArray();
            foreach (var peer in peers)
            {
                ArgumentNullException.ThrowIfNull(peer);
                if (peer == this || peer.EditDisposed || !SupportsEditControls || !peer.SupportsEditControls ||
                    !IsEditNumber(ViewType) || !IsEditNumber(peer.ViewType))
                    throw new ArgumentException("Chains require distinct numeric leaves.", nameof(value));
            }
            if (ChainPeers.SetEquals(peers)) return;
            ClearChain();
            foreach (var peer in ChainPeers.ToArray())
            {
                peer.ClearChain();
                peer.ChainPeers.Remove(this);
                peer.EditingChanged(nameof(ChainsWith));
            }
            ChainPeers.Clear();
            foreach (var peer in peers)
            {
                peer.ClearChain();
                ChainPeers.Add(peer);
                peer.ChainPeers.Add(this);
                peer.EditingChanged(nameof(ChainsWith));
            }
            EditingChanged(nameof(ChainsWith));
        }
    }

    // Includes this node: Count == 1 is an armed chain, with no propagated writes yet.
    public IReadOnlyList<InspectorNode> ChainedWith => Chain ?? Array.Empty<InspectorNode>();

    public void EnableChain()
    {
        ObjectDisposedException.ThrowIf(EditDisposed, this);
        if (ChainPeers.Count == 0) throw new InvalidOperationException("This field has no chain peers.");
        if (Chain != null) return;
        var members = new List<InspectorNode> { this };
        foreach (var peer in ChainPeers)
            if (peer.Chain is { } active)
                foreach (var member in active)
                    if (!members.Contains(member)) members.Add(member);
        var group = Array.AsReadOnly(members.ToArray());
        foreach (var member in members) member.Chain = group;
        foreach (var member in members) member.EditingChanged(nameof(ChainedWith));
    }

    public void ClearChain()
    {
        if (Chain is not { } members) return;
        foreach (var member in members) member.Chain = null;
        foreach (var member in members) member.EditingChanged(nameof(ChainedWith));
    }

    public InspectorEditGesture BeginEdit() => new(this);

    // User-originated absolute edit. Normal SetValue/SetValues never propagate.
    public InspectorEditResult EditValue(object? value)
    {
        using var edit = BeginEdit();
        return edit.SetValue(value);
    }

    internal void CheckDirectEdit()
    {
        ObjectDisposedException.ThrowIf(EditDisposed, this);
        if (IsEditLocked || ReadOnly || IsCompromised || ReadFailed || !SupportsEditControls || Inspector.Instances.Count == 0)
            throw new InvalidOperationException($"'{Path}' cannot receive a direct UI edit.");
    }

    internal static bool IsEditNumber(Type? type) => type != null &&
        ValueConverter.IsNumber(Nullable.GetUnderlyingType(type) ?? type);

    internal void ResetEditing()
    {
        ClearChain();
        IsEditLocked = false;
        ++EditVersion;
    }

    internal void RestoreEditPending(bool wasPending)
    {
        if (!wasPending && !Inspector.Options.ViewToInstance && HasPendingValue && Failure == null)
            DropPending();
    }

    private void EditingChanged(string option)
    {
        if (option == nameof(ChainsWith)) ChainPeerView = Array.AsReadOnly(ChainPeers.ToArray());
        ++EditVersion;
        OnOptionChanged(option);
    }

    private void DisposeEditing()
    {
        if (EditDisposed) return;
        ChainsWith = [];
        ClearChain();
        EditDisposed = true;
        ++EditVersion;
    }
}
