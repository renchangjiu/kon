using System;
using System.IO;
using System.Reactive;
using Avalonia.Animation;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using kon.Components;
using kon.Enums;
using kon.Models;
using kon.Utils;
using NLog;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace kon.ViewModels;

public class PlayBarViewModel : ViewModelBase {

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    private Playlist playlist;

    private Player player;

    [Reactive]
    public Music? CurrentMusic { get; set; }

    [Reactive]
    public int Position { get; set; }

    [Reactive]
    public string PositionFormatted { get; set; } = "00:00";


    public PlayBarViewModel(Playlist playlist, Player player) {
        this.playlist = playlist;
        this.player = player;

        this.playlist.OnCurrentMusicChanged += (sender, m) => {
            CurrentMusic = m;
            Log.Info("OnCurrentMusicChanged");
        };

        this.player.OnPositionChanged += (sender, pos) => {
            Position = pos;
            PositionFormatted = CommonUtils.formatDuration(pos);
        };

        this.player.OnStateChanged += (sender, state) => {
            // if (state == PlayState.Run) {
            //     PlayBm = PlayingBm;
            //     PlayHoverBm = PlayingHoverBm;
            // } else {
            //     PlayBm = PausedBm;
            //     PlayHoverBm = PausedHoverBm;
            // }
        };

        this.WhenAnyValue(model => model.CurrentMusic)
            .Subscribe(o => {
                if (CurrentMusic != null) {
                    // this.player.play(CurrentMusic);
                }
            });
        this.WhenAnyValue(model => model.Position)
            .Subscribe(pos => {
                if (Math.Abs(pos - this.player.Position) >= 3) {
                    this.player.Position = pos;
                }

                Log.Debug(pos + "-" + this.player.Position);
            });
    }

    /// <summary>
    /// only for designer preview
    /// </summary>
    public PlayBarViewModel() {
        CurrentMusic = CommonUtils.ParseToMusic("D:/CloudMusic/Akie秋绘 - はるのとなり（《摇曳露营\u25b3》第二季ED）（翻自 佐々木恵梨）.mp3");
        // playlist.Mode = PlayMode.Order;
    }

    public void HandleMode() {

    }

    public void HandlePlay() {
        if (CurrentMusic != null) {
            player.play(CurrentMusic);
        }
    }

    public void HandlePrev() {
        playlist.prev();
        HandlePlay();
    }

    public void HandleNext() {
        playlist.next();
        HandlePlay();
    }


    public void switchPlaylistViewVisible() {
        MainContentViewModel vm = App.getService<MainContentViewModel>();
        vm.SwitchPlaylistViewVisible();
    }

}