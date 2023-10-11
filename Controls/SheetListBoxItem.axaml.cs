using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace kon.Controls;

public partial class SheetListBoxItem : UserControl {

    public static readonly StyledProperty<IImage> SourceProperty = AvaloniaProperty.Register<SheetListBoxItem, IImage>(
        nameof(Source));

    public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<SheetListBoxItem, string>(
        nameof(Text));

    public string Text {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public IImage Source {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public SheetListBoxItem() {
        InitializeComponent();
    }

}