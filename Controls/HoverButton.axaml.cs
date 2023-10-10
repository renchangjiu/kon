using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace kon.Controls;

public partial class HoverButton : UserControl {

    public static readonly StyledProperty<IImage> SourceProperty = AvaloniaProperty.Register<HoverButton, IImage>(
        "Source");

    public static readonly StyledProperty<IImage> HoverSourceProperty = AvaloniaProperty.Register<HoverButton, IImage>(
        "HoverSource");

    public static readonly StyledProperty<ICommand?> CommandProperty =
        AvaloniaProperty.Register<HoverButton, ICommand?>(
            nameof(Command));

    public ICommand? Command {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public IImage HoverSource {
        get => GetValue(HoverSourceProperty);
        set => SetValue(HoverSourceProperty, value);
    }


    public IImage Source {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public HoverButton() {
        InitializeComponent();
    }

    private void OnPointerEntered(object? sender, PointerEventArgs e) {
        ThisImage.Source = HoverSource;
    }

    private void OnPointerExited(object? sender, PointerEventArgs e) {
        ThisImage.Source = Source;
    }

}