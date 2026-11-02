using System.Numerics;
using Hexa.NET.ImGui;
using PixieInspector.Options;
using PixieInspector.Views;
using Gui = Hexa.NET.ImGui.ImGui;

namespace PixieInspector.ImGui;

internal sealed class LabelScrub(ImGuiRow row)
{
    private InspectorEditGesture? Gesture;
    private double Written;
    private bool Cancelled;

    public void Update()
    {
        var active = Gui.IsItemActive();
        if (Gesture is { } gesture && row.View.OwnsInteraction(gesture) && Gui.IsKeyPressed(ImGuiKey.Escape, false))
        {
            Gesture = null;
            Cancelled = active;
            row.Write(() => { row.View.EndInteraction(cancel: true); if (gesture.LastResult is { } result) row.CheckResult(result); });
            return;
        }
        var scrubs = ViewRules.CanScrub(row.Node);
        if (!active) Cancelled = false;
        if (!active || !scrubs) Gesture = null;
        if (!scrubs) return;
        var vertical = row.Node.ScrubAxis == ScrubAxis.Vertical;
        if (Gesture != null || Gui.IsItemHovered())
            Gui.SetMouseCursor(vertical ? ImGuiMouseCursor.ResizeNs : ImGuiMouseCursor.ResizeEw);
        if (!active || Cancelled) return;
        var drag = Gui.GetMouseDragDelta(ImGuiMouseButton.Left, 3f);
        if (Gesture == null)
        {
            if (drag == Vector2.Zero) return;
            if (!row.Write(() => Gesture = row.View.BeginInteraction(row.Node))) return;
            Written = 0;
        }
        var io = Gui.GetIO();
        var step = (row.Node.ScrubMultiplier ?? 0) * (io.KeyShift ? 10 : io.KeyCtrl ? 0.1 : 1);
        var delta = (vertical ? -drag.Y : drag.X) * step;
        if (delta == Written) return;
        Written = delta;
        row.Write(() => row.CheckResult(Gesture!.SetDelta(delta)));
    }
}
