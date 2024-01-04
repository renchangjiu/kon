using System;
using Avalonia.Animation;
using Avalonia.Media.Imaging;
using Avalonia.ReactiveUI;
using kon.Components;
using kon.Enums;
using kon.Utils;
using kon.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace kon.Views;

public partial class PlayBarView : ReactiveUserControl<PlayBarViewModel> {

    public Playlist playlist { get; }

    private Player player { get; }

    public PlayBarView() {
        InitializeComponent();

        playlist = App.getService<Playlist>();
        player = App.getService<Player>();


        this.WhenAnyValue(view => view.player.State)
            .Subscribe(state => {
                // if (state == PlayState.Run) {
                //     PlayBtn.Source = PlayingBm;
                //     PlayBtn.HoverSource = PlayingHoverBm;
                // } else {
                //     PlayBtn.Source = PausedBm;
                //     PlayBtn.HoverSource = PausedHoverBm;
                // }
            });


        PlayBarViewModel vm = ViewModel!;
        this.WhenAnyValue(view => view.ViewModel.Position).Subscribe(i => {
            // Console.WriteLine("view" + i);
        });
        Array values = Enum.GetValues(typeof(PlayMode));
    }

}