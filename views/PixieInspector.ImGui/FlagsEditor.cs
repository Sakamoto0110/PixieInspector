using PixieInspector.Options;
using Gui = Hexa.NET.ImGui.ImGui;

namespace PixieInspector.ImGui;

internal sealed class FlagsEditor(ImGuiRow row) : ImGuiEditor(row)
{
    private IReadOnlyList<FlagChoice>? Choices;

    public override void Draw()
    {
        if (!Gui.BeginCombo("##value", Node.IsMixed ? MixedText : Format(Node.ViewValue)))
        {
            Choices = null;
            return;
        }
        try
        {
            if (Choices == null || Gui.IsWindowAppearing())
                Choices = Node.GetFlags();
            for (var i = 0; i < Choices.Count; ++i)
            {
                var choice = Choices[i];
                var state = Node.GetFlagState(choice.Mask);
                var bits = state == true ? 3 : state == false ? 0 : 1;
                Gui.PushID(i);
                try
                {
                    var changed = Gui.CheckboxFlags("##flag", ref bits, 3);
                    Gui.SameLine();
                    Gui.TextUnformatted(choice.Label);
                    if (changed)
                        Row.Write(() => Node.SetFlag(choice.Mask, bits == 3));
                }
                finally { Gui.PopID(); }
            }
        }
        finally { Gui.EndCombo(); }
    }
}
