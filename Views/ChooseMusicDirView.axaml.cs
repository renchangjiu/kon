using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using kon.ViewModels;

namespace kon.Views;

public partial class ChooseMusicDirView : ReactiveUserControl<ChooseMusicDirViewModel> {

    public ChooseMusicDirView() {
        InitializeComponent();
    }

}