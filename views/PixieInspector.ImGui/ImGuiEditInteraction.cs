using Gui = Hexa.NET.ImGui.ImGui;

namespace PixieInspector.ImGui;

public sealed partial class ImGuiInspectorView
{
    private InspectorEditGesture? Editing;
    public ImGuiEditIcons? EditIcons { get; set; }
    public bool IsEditing => Editing != null;
    public event EventHandler<ImGuiEditInteractionEventArgs>? InteractionStarted;
    public event EventHandler<ImGuiEditInteractionEventArgs>? InteractionFinished;

    // Host-owned custom rows can share the same controls. Leaves the cursor on the label line.
    // The host still owns that row's mutation path and must honor IsEditLocked for direct actions.
    public void DrawEditControls(InspectorNode node)
    {
        ArgumentNullException.ThrowIfNull(node);
        ObjectDisposedException.ThrowIf(Inspector == null || node.EditDisposed, this);
        Gui.PushID(node.Inspector.Id);
        Gui.PushID(node.Path);
        try
        {
            if (!RowsByNode.TryGetValue(node, out var row) || !row.Fits())
                RowsByNode[node] = row = new ImGuiRow(node, this);
            row.DrawEditControls();
        }
        finally { Gui.PopID(); Gui.PopID(); }
    }

    internal InspectorEditGesture BeginInteraction(InspectorNode node)
    {
        if (Editing?.Node == node) return Editing;
        EndInteraction();
        Editing = node.BeginEdit();
        try { InteractionStarted?.Invoke(this, new(Editing)); }
        catch { Editing.Dispose(); Editing = null; throw; }
        return Editing;
    }

    // Hosts can end a gesture before selection changes. Abandonment never writes into a replaced World.
    public void EndInteraction(bool cancel = false, bool abandon = false)
    {
        if (Editing is not { } gesture) return;
        Editing = null;
        InspectorEditResult? result = gesture.LastResult;
        Exception? error = null;
        try { if (cancel && !abandon) { result = null; result = gesture.Cancel(); } }
        catch (Exception e) { error = e; throw; }
        finally
        {
            gesture.Dispose();
            InteractionFinished?.Invoke(this, new(gesture, cancel || abandon, cancel && !abandon, result, error));
        }
    }

    internal InspectorEditResult WriteValue(InspectorNode node, object? value) => BeginInteraction(node).SetValue(value);
    internal bool OwnsInteraction(InspectorEditGesture gesture) => ReferenceEquals(Editing, gesture);

    private void FinishInactiveInteraction()
    {
        if (Editing != null && !Gui.IsAnyItemActive()) EndInteraction();
    }
}
