using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using kon.ViewModels;
using NLog;

namespace kon.Views;

public partial class SidebarView : UserControl {

    public SidebarView() {
        InitializeComponent();
    }


    private void onAddBtnClicked(object? sender, PointerPressedEventArgs e) {
        SheetAddWindow dialog = new SheetAddWindow();
        dialog.DataContext = App.getService<SheetAddViewModel>();
        var showDialog = dialog.ShowDialog((Window)VisualRoot);
    }

}