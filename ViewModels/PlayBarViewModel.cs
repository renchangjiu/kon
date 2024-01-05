using System;
using System.IO;
using System.Reactive;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Media;
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

    private readonly Playlist playlist;

    private readonly Player player;

    [Reactive]
    public Music? CurrentMusic { get; set; }

    [Reactive]
    public int Position { get; set; }

    [Reactive]
    public Geometry PlayButtonData { get; set; }

    [Reactive]
    public Geometry? ModeButtonData { get; set; }

    [Reactive]
    public string PositionFormatted { get; set; } = "00:00";


    public PlayBarViewModel(Playlist playlist, Player player) {
        init();

        this.playlist = playlist;
        this.player = player;

        this.playlist.OnCurrentMusicChanged += (sender, m) => {
            CurrentMusic = m;
            Log.Debug(nameof(playlist.OnCurrentMusicChanged));
        };

        this.playlist.OnPlayModeChanged += (sender, mode) => {
            ModeButtonData = (Geometry?)App.Current.FindResource("Playbar" + mode.ToString());
            Log.Debug(nameof(playlist.OnPlayModeChanged));
        };


        this.player.OnPositionChanged += (sender, pos) => {
            Position = pos;
            PositionFormatted = CommonUtils.formatDuration(pos);
        };
        PlayButtonData = (Geometry?)App.Current.FindResource("PlaybarPlay");
        this.player.OnStateChanged += (sender, state) => {
            Log.Debug("OnStateChanged");
            if (state == PlayState.Run) {
                PlayButtonData = (Geometry?)App.Current.FindResource("PlaybarPause");
            } else {
                PlayButtonData = (Geometry?)App.Current.FindResource("PlaybarPlay");
            }
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

                // Log.Debug(pos + "-" + this.player.Position);
            });
    }

    /// <summary>
    /// only for designer preview
    /// </summary>
    public PlayBarViewModel() {
        this.init();
        CurrentMusic = CommonUtils.ParseToMusic("D:/CloudMusic/Akie秋绘 - はるのとなり（《摇曳露营\u25b3》第二季ED）（翻自 佐々木恵梨）.mp3");
        // playlist.Mode = PlayMode.Order;
    }

    private void init() {
    }

    public void HandleMode() {
        playlist.toNextMode();
    }

    public void HandleLyric() {
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

    public void HandleToPlayPage() {
        App.getService<MainWindowViewModel>().SwitchToPlayPage();
    }

    public void switchPlaylistViewVisible() {
        MainContentViewModel vm = App.getService<MainContentViewModel>();
        vm.SwitchPlaylistViewVisible();
    }

}