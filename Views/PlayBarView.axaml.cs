using Avalonia.ReactiveUI;
using kon.ViewModels;

namespace kon.Views;

public partial class PlayBarView : ReactiveUserControl<PlayBarViewModel> {

    public PlayBarView() {
        InitializeComponent();
    }

}