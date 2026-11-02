namespace PixieInspector.ImGui;

public sealed class ImGuiEditInteractionEventArgs(InspectorEditGesture gesture, bool cancelled = false,
    bool rollbackAttempted = false, InspectorEditResult? result = null, Exception? error = null) : EventArgs
{
    public InspectorEditGesture Gesture { get; } = gesture;
    public bool Cancelled { get; } = cancelled;
    public bool RollbackAttempted { get; } = rollbackAttempted;
    public InspectorEditResult? Result { get; } = result;
    public Exception? Error { get; } = error;
}
