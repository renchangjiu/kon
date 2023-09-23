using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace kon.Controls;

public partial class HoverImage : UserControl {

    public static readonly StyledProperty<IImage> SourceProperty = AvaloniaProperty.Register<HoverImage, IImage>(
        "Source");

    public static readonly StyledProperty<IImage> HoverSourceProperty = AvaloniaProperty.Register<HoverImage, IImage>(
        "HoverSource");

    public IImage HoverSource {
        get => GetValue(HoverSourceProperty);
        set => SetValue(HoverSourceProperty, value);
    }


    public IImage Source {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public HoverImage() {
        InitializeComponent();
    }

    private void OnPointerEntered(object? sender, PointerEventArgs e) {
        Image? image = sender as Image;
        image!.Source = HoverSource;
    }

    private void OnPointerExited(object? sender, PointerEventArgs e) {
        Image image = (sender as Image)!;
        image.Source = Source;
    }

}