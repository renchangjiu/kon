using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using kon.ViewModels;

namespace kon.Views;

public partial class PlayBarView : ReactiveUserControl<PlayBarViewModel> {

    public PlayBarView() {
        InitializeComponent();
    }


    private void onPlayBtnPressed(object? sender, TappedEventArgs e) {
        ViewModel?.handlePlay();
    }

}