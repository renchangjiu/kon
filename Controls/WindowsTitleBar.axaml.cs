using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using kon.Utils;
using kon.Views;

namespace kon.Controls;

public partial class WindowsTitleBar : UserControl {

    public WindowsTitleBar() {
        InitializeComponent();
        MinimizeButton.Click += MinimizeWindow;
        CloseButton.Click += CloseWindow;
        MaximizeButton.Click += MaximizeWindow;
        SubscribeToWindowState();
    }

    private void MaximizeWindow(object? sender, RoutedEventArgs e) {
        Window? window = VisualRoot as Window;
        if (window.WindowState == WindowState.Normal) {
            window.WindowState = WindowState.Maximized;
        } else {
            window.WindowState = WindowState.Normal;
        }
    }

    private void MinimizeWindow(object? sender, RoutedEventArgs e) {
        Window? window = VisualRoot as Window;
        window!.WindowState = WindowState.Minimized;
    }

    private void CloseWindow(object? sender, RoutedEventArgs e) {
        Window? window = VisualRoot as Window;
        window?.Close();
    }


    private void OnPointerPressed(object? sender, PointerPressedEventArgs e) {
        UiUtils.GetMainWindow(this).BeginMoveDrag(e);
    }

    private async void SubscribeToWindowState() {
        Window hostWindow = (Window)this.VisualRoot;

        while (hostWindow == null) {
            hostWindow = (Window)this.VisualRoot;
            await Task.Delay(50);
        }

        hostWindow.GetObservable(Window.WindowStateProperty).Subscribe(s => {
            if (s != WindowState.Maximized) {
                MaximizeIcon.Data =
                    Geometry.Parse("M2048 2048v-2048h-2048v2048h2048zM1843 1843h-1638v-1638h1638v1638z");
                hostWindow.Padding = new Thickness(0, 0, 0, 0);
                MaximizeToolTip.Content = "Maximize";
            }

            if (s == WindowState.Maximized) {
                MaximizeIcon.Data =
                    Geometry.Parse(
                        "M2048 1638h-410v410h-1638v-1638h410v-410h1638v1638zm-614-1024h-1229v1229h1229v-1229zm409-409h-1229v205h1024v1024h205v-1229z");
                hostWindow.Padding = new Thickness(7, 7, 7, 7);
                MaximizeToolTip.Content = "Restore Down";

                // This should be a more universal approach in both cases, but I found it to be less reliable, when for example double-clicking the title bar.
                /*hostWindow.Padding = new Thickness(
                        hostWindow.OffScreenMargin.Left,
                        hostWindow.OffScreenMargin.Top,
                        hostWindow.OffScreenMargin.Right,
                        hostWindow.OffScreenMargin.Bottom);*/
            }
        });
    }

    private void OnDoubleTapped(object? sender, TappedEventArgs e) {
        MaximizeWindow(sender, e);
    }

}