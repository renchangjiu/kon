using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using kon.ViewModels;

namespace kon.Views;

public partial class PlaylistView : ReactiveUserControl<PlaylistViewModel> {

    public PlaylistView() {
        InitializeComponent();
    }

}