﻿using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using kon.Utils;
using kon.Views;
using NLog;

namespace kon.Controls;

public partial class WindowsTitleBar : UserControl {

    public WindowsTitleBar() {
        InitializeComponent();
        MinimizeButton.Tapped += MinimizeWindow;
        CloseButton.Tapped += CloseWindow;
        MaximizeBtn.Tapped += MaximizeWindow;
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
        if (sender is DockPanel) {
            UiUtils.GetWindow(this).BeginMoveDrag(e);
        }
    }

    private async void SubscribeToWindowState() {
        Window window = (Window)this.VisualRoot;

        while (window == null) {
            window = (Window)this.VisualRoot;
            await Task.Delay(50);
        }

        window.GetObservable(Window.WindowStateProperty).Subscribe(s => {
            if (s != WindowState.Maximized) {
                MaximizeBtn.Source = CommonUtils.getBitmapFromAsset("/Assets/Icons/maximize.png");
                MaximizeBtn.HoverSource = CommonUtils.getBitmapFromAsset("/Assets/Icons/maximize_hover.png");
            }

            if (s == WindowState.Maximized) {
                MaximizeBtn.Source = CommonUtils.getBitmapFromAsset("/Assets/Icons/maximize_cancel.png");
                MaximizeBtn.HoverSource = CommonUtils.getBitmapFromAsset("/Assets/Icons/maximize_cancel_hover.png");
            }
        });
    }

    private void OnDoubleTapped(object? sender, TappedEventArgs e) {
        MaximizeWindow(sender, e);
    }

}