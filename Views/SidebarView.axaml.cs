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

    protected override void OnPointerEntered(PointerEventArgs e) {
        base.OnPointerEntered(e);
        MyScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
    }

    protected override void OnPointerExited(PointerEventArgs e) {
        base.OnPointerExited(e);
        MyScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
    }

}