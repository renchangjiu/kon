using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using FluentAvalonia.UI.Controls;
using kon.Models;
using kon.ViewModels;

namespace kon.Views;

public partial class SidebarView : ReactiveUserControl<SidebarViewModel> {

    public SidebarView() {
        InitializeComponent();
    }


    private void handleDeleteSheet(object? sender, PointerPressedEventArgs e) {
        MenuFlyoutItem item = (sender as MenuFlyoutItem)!;
        Sheet sheet = (item.Tag as Sheet)!;
        ViewModel!.doDeleteSheet(sheet.Id);
    }

}