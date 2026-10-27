using Avalonia.Controls;

namespace PixieInspector.Avalonia;

internal sealed class FlagsEditor : AvaloniaEditor
{
    private readonly Button Box = new();
    private readonly TextBlock Caption = new();
    private readonly StackPanel Panel = new();
    private readonly Flyout Popup;
    private readonly List<(ulong Mask, CheckBox Check)> Items = [];

    public FlagsEditor(AvaloniaRow row) : base(row)
    {
        Popup = new Flyout { Content = new ScrollViewer { Content = Panel, MaxHeight = 320 } };
        Box.Content = Caption;
        Box.Click += (_, _) => Row.Try(() => { Fill(); Popup.ShowAt(Box); });
    }

    public override Control Control => Box;
    public override bool ShowsErrors => false;

    private void Fill()
    {
        Items.Clear();
        Panel.Children.Clear();
        foreach (var choice in Node.GetFlags())
        {
            var check = new CheckBox { Content = new TextBlock { Text = choice.Label } };
            var mask = choice.Mask;
            check.Click += (_, _) =>
            {
                Row.Write(() => Node.SetFlag(mask, Node.GetFlagState(mask) != true));
                Row.Show();
            };
            Items.Add((mask, check));
            Panel.Children.Add(check);
        }
        Row.Show();
    }

    public override void ShowValue()
    {
        Caption.Text = Node.IsMixed ? "—" : Format(Node.ViewValue);
        foreach (var (mask, check) in Items) check.IsChecked = Node.GetFlagState(mask);
    }

    public override void ShowState(bool readOnly, bool enabled)
    {
        base.ShowState(readOnly, enabled);
        if (readOnly || !enabled) Popup.Hide();
    }

    public override void Dispose() { Popup.Hide(); Panel.Children.Clear(); Items.Clear(); }
}
