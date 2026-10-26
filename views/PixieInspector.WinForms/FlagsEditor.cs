using System.Windows.Forms;

namespace PixieInspector.WinForms;

internal sealed class FlagsEditor : WinFormsEditor
{
    private readonly Button Box = new() { UseMnemonic = false };
    private readonly ContextMenuStrip Popup = new();
    private readonly List<(ulong Mask, ToolStripMenuItem Item)> Items = [];

    public FlagsEditor(WinFormsRow row) : base(row)
    {
        Box.Click += (_, _) => Row.Try(() => { Fill(); Popup.Show(Box, 0, Box.Height); });
    }

    public override Control Control => Box;

    private void Fill()
    {
        // Disposing old items also releases their handlers and captured masks.
        foreach (var item in Items) item.Item.Dispose();
        Items.Clear();
        Popup.Items.Clear();
        foreach (var choice in Node.GetFlags())
        {
            var item = new ToolStripMenuItem(choice.Label.Replace("&", "&&"));
            var mask = choice.Mask;
            item.Click += (_, _) =>
            {
                Row.Write(() => Node.SetFlag(mask, Node.GetFlagState(mask) != true));
                Row.Show();
            };
            Items.Add((mask, item));
            Popup.Items.Add(item);
        }
        Row.Show();
    }

    public override void ShowValue()
    {
        Box.Text = Node.IsMixed ? "—" : Format(Node.ViewValue);
        foreach (var (mask, item) in Items)
            item.CheckState = Node.GetFlagState(mask) switch
            { true => CheckState.Checked, false => CheckState.Unchecked, null => CheckState.Indeterminate };
    }

    public override void ShowState(bool readOnly, bool enabled, bool failed)
    {
        base.ShowState(readOnly, enabled, failed);
        if (readOnly || !enabled) Popup.Close();
    }

    public override void Dispose()
    {
        Popup.Dispose();
        base.Dispose();
    }
}
