using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace kon.Controls;

public partial class SVGButton : UserControl {

    public static readonly StyledProperty<Geometry> DataProperty =
        AvaloniaProperty.Register<SVGButton, Geometry>(nameof(Data));


    public static readonly StyledProperty<IBrush> PointerOverForegroundProperty =
        AvaloniaProperty.Register<SVGButton, IBrush>(nameof(PointerOverForeground));


    public static readonly StyledProperty<ICommand?> CommandProperty =
        AvaloniaProperty.Register<HoverButton, ICommand?>(
            nameof(Command));

    public ICommand? Command {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }


    public SVGButton() {
        InitializeComponent();
    }

    private void OnPointerEntered(object? sender, PointerEventArgs e) {
        ThisImage.Foreground = PointerOverForeground;
    }

    private void OnPointerExited(object? sender, PointerEventArgs e) {
        ThisImage.Foreground = Foreground;
    }

    public Geometry Data {
        get => GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    public IBrush PointerOverForeground {
        get => GetValue(PointerOverForegroundProperty);
        set => SetValue(PointerOverForegroundProperty, value);
    }

}