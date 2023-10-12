using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using kon.Models;
using kon.ViewModels;

namespace kon.Views;

public partial class SheetLocalInfoView : ReactiveUserControl<SheetLocalInfoViewModel> {

    public SheetLocalInfoView() {
        InitializeComponent();
    }

    private void OnGridDoubleTapped(object? sender, TappedEventArgs e) {
        DataGrid grid = (sender as DataGrid)!;
        Music item = ((Music)grid.SelectedItem);
        ViewModel?.OnReplacePlaylist(item.Index);
    }

}