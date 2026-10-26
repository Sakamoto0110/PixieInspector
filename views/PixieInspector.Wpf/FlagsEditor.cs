using System.Windows.Controls;

namespace PixieInspector.Wpf;

internal sealed class FlagsEditor : WpfEditor
{
    private readonly Button Box = new();
    private readonly TextBlock Caption = new();
    private readonly ContextMenu Popup = new();
    private readonly List<(ulong Mask, CheckBox Check)> Items = [];

    public FlagsEditor(WpfRow row) : base(row)
    {
        Box.ContextMenu = Popup;
        Box.Content = Caption;
        Box.Click += (_, _) => Row.Try(() =>
        {
            Fill();
            Popup.PlacementTarget = Box;
            Popup.IsOpen = true;
        });
    }

    public override System.Windows.FrameworkElement Control => Box;

    private void Fill()
    {
        Items.Clear();
        Popup.Items.Clear();
        foreach (var choice in Node.GetFlags())
        {
            var check = new CheckBox { Content = new TextBlock { Text = choice.Label }, IsHitTestVisible = false, Focusable = false };
            var item = new MenuItem { Header = check, StaysOpenOnClick = true };
            var mask = choice.Mask;
            item.Click += (_, _) =>
            {
                Row.Write(() => Node.SetFlag(mask, Node.GetFlagState(mask) != true));
                Row.Show();
            };
            Items.Add((mask, check));
            Popup.Items.Add(item);
        }
        Row.Show();
    }

    public override void ShowValue()
    {
        Caption.Text = Node.IsMixed ? "—" : Format(Node.ViewValue);
        foreach (var (mask, check) in Items) check.IsChecked = Node.GetFlagState(mask);
    }

    public override void ShowState(bool readOnly, bool enabled, bool failed)
    {
        base.ShowState(readOnly, enabled, failed);
        if (readOnly || !enabled) Popup.IsOpen = false;
    }

    public override void Dispose() { Popup.IsOpen = false; Popup.Items.Clear(); Items.Clear(); }
}
