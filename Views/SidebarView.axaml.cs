using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
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

    private void ListBox1_OnSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        ListBox? box = sender as ListBox;
        int idx = box.SelectedIndex;
        if (idx < 0) {
            return;
        }

        List<ILogical> items = new(box.GetLogicalChildren());
        ListBoxItem item = items[idx] as ListBoxItem;

        List<Visual> visuals = new(item.GetVisualChildren());
        Visual panel = visuals[0];
        Visual visual = new List<Visual>(panel.GetVisualChildren())[1];
        Rectangle? rect = visual as Rectangle;
        rect.IsVisible = false;
    }

    private Rectangle get(ListBox box) {
        return null;
    }

}