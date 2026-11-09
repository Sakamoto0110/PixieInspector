using System.Numerics;
using Hexa.NET.ImGui;
using Gui = Hexa.NET.ImGui.ImGui;

namespace PixieInspector.ImGui;

internal sealed partial class ImGuiRow
{
    internal void DrawEditControls()
    {
        if (Kind is Options.EditorKind.Header or Options.EditorKind.Separator) return;
        var size = new Vector2(Gui.GetFrameHeight());
        if (Node.SupportsEditControls)
        {
            var clicked = Gui.InvisibleButton("##edit-lock", size);
            DrawEditIcon(false, size, Node.IsEditLocked ? Gui.GetColorU32(ImGuiCol.CheckMark) : Gui.GetColorU32(ImGuiCol.TextDisabled));
            ImGuiInspectorView.Tooltip(Node.IsEditLocked
                ? "Direct editing is locked. Linked edits can still update this field. Click to unlock."
                : "Click to lock direct editing (including scrubbing). Linked edits remain allowed.");
            if (clicked) Write(() => { View.EndInteraction(); Node.IsEditLocked = !Node.IsEditLocked; });
        }
        else Gui.Dummy(size);
        Gui.SameLine(0, 2);
        if (Node.ChainsWith.Count > 0)
        {
            var clicked = Gui.InvisibleButton("##edit-chain", size);
            var active = Node.IsChainEnabled;
            DrawEditIcon(true, size, active ? Gui.GetColorU32(ImGuiCol.CheckMark) : Gui.GetColorU32(ImGuiCol.TextDisabled));
            ImGuiInspectorView.Tooltip(!active ? "Enable this field's chain. The first click arms one field without changing values."
                : Node.ChainedWith.Count == 1 ? "Chain armed with one field. Click another eligible field to join. Right-click clears the chain."
                : $"Linked group: {Node.ChainedWith.Count} fields. Absolute edits share a value; scrubs share a delta. Right-click clears the entire chain.");
            if (Gui.IsItemClicked(ImGuiMouseButton.Right) && active)
                Write(() => { View.EndInteraction(); Node.ClearChain(); });
            else if (clicked && !active)
                Write(() => { View.EndInteraction(); Node.EnableChain(); });
        }
        else Gui.Dummy(size);
        Gui.SameLine(0, 2);
    }

    private void DrawEditIcon(bool chain, Vector2 size, uint color)
    {
        var p = Gui.GetItemRectMin() + new Vector2(3);
        var end = Gui.GetItemRectMax() - new Vector2(3);
        var draw = Gui.GetWindowDrawList();
        if (View.EditIcons is { } icons)
        {
            draw.AddImage(chain ? icons.Chain : icons.Lock, p, end, Vector2.Zero, Vector2.One, color);
            return;
        }
        var s = (size.X - 6) / 16;
        if (chain)
        {
            draw.AddRect(p + new Vector2(0, 4) * s, p + new Vector2(10, 12) * s, color, 3 * s, 0, 1.5f);
            draw.AddRect(p + new Vector2(6, 4) * s, p + new Vector2(16, 12) * s, color, 3 * s, 0, 1.5f);
        }
        else
        {
            draw.AddRect(p + new Vector2(4, 0) * s, p + new Vector2(12, 11) * s, color, 4 * s, 0, 1.5f);
            draw.AddRectFilled(p + new Vector2(1, 7) * s, p + new Vector2(15, 16) * s, color, 2 * s);
        }
    }
}
