using System;
using System.IO;
using System.Reactive;
using Avalonia.Animation;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using kon.Components;
using kon.Models;
using kon.Utils;
using NLog;
using ReactiveUI;

namespace kon.ViewModels;

public class PlayBarViewModel : ViewModelBase {

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();


    private Music? currentCurrentMusic;

    private Bitmap _defaultCover;

    private Playlist playlist;

    private Player player;


    private int _position;
    private string _positionFormatted = "00:00";
    private Bitmap _playBm = PausedBm;
    private Bitmap _playHoverBm = PausedHoverBm;

    private static readonly Bitmap PlayingBm = CommonUtils.getBitmapFromAsset("/Assets/Icons/playbar_play.png");

    private static readonly Bitmap PlayingHoverBm =
        CommonUtils.getBitmapFromAsset("/Assets/Icons/playbar_play_hover.png");

    private static readonly Bitmap PausedBm = CommonUtils.getBitmapFromAsset("/Assets/Icons/playbar_pause.png");

    private static readonly Bitmap PausedHoverBm =
        CommonUtils.getBitmapFromAsset("/Assets/Icons/playbar_pause_hover.png");

    public PlayBarViewModel(Playlist playlist, Player player) {
        this.playlist = playlist;
        this.player = player;

        this.playlist.CurrentMusicChanged += mc => {
            CurrentMusic = mc;
            Log.Info("CurrentMusicChanged");
        };

        this.player.OnPositionChanged += (sender, pos) => {
            Position = pos;
            PositionFormatted = CommonUtils.formatDuration(pos);
        };

        this.player.OnStateChanged += (sender, state) => {
            if (state == PlayState.Run) {
                PlayBm = PlayingBm;
                PlayHoverBm = PlayingHoverBm;
            } else {
                PlayBm = PausedBm;
                PlayHoverBm = PausedHoverBm;
            }
        };

        // this.playlist.addMusic(CommonUtils.ParseToMusic("C:/Users/su/Desktop/放課後ティータイム - Listen!!.flac"));
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


        // Stream fs = AssetLoader.Open(new Uri("avares://kon/Assets/Icons/default_music_image.png"));
        // DefaultCover = new Bitmap(fs);
    }

    public void handlePlay() {
        if (CurrentMusic != null) {
            player.play(CurrentMusic);
        }
    }

    public Music? CurrentMusic {
        get => currentCurrentMusic;
        set => this.RaiseAndSetIfChanged(ref currentCurrentMusic, value);
    }

    public Bitmap DefaultCover {
        get => _defaultCover;
        set => this.RaiseAndSetIfChanged(ref _defaultCover, value);
    }

    public int Position {
        get => _position;
        set => this.RaiseAndSetIfChanged(ref _position, value);
    }

    public string PositionFormatted {
        get => _positionFormatted;
        set => this.RaiseAndSetIfChanged(ref _positionFormatted, value);
    }

    public Bitmap PlayBm {
        get => _playBm;
        set => this.RaiseAndSetIfChanged(ref _playBm, value);
    }

    public Bitmap PlayHoverBm {
        get => _playHoverBm;
        set => this.RaiseAndSetIfChanged(ref _playHoverBm, value);
    }

}