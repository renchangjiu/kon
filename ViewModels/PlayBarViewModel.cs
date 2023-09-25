using System;
using System.IO;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using kon.Components;
using kon.Models;
using kon.Utils;
using NLog;
using ReactiveUI;

namespace kon.ViewModels;

public class PlayBarViewModel : ViewModelBase {

    private Music? _music;

    private Bitmap _defaultCover;

    private Playlist playlist;

    private Logger log;

    public PlayBarViewModel(Playlist playlist) {
        log = LogManager.GetCurrentClassLogger();

        this.playlist = playlist;

        this.playlist.CurrentMusicChanged += mc => {
            Music = mc;
            log.Info("CurrentMusicChanged");
        };

        this.playlist.addMusic(CommonUtils.ParseToMusic("C:/Users/su/Desktop/放課後ティータイム - Listen!!.flac"));
        this.WhenAnyValue(model => model.Music)
            .Subscribe(o => { });
        // Stream fs = AssetLoader.Open(new Uri("avares://kon/Assets/Icons/default_music_image.png"));
        // DefaultCover = new Bitmap(fs);
    }


    public Music? Music {
        get => _music;
        set => this.RaiseAndSetIfChanged(ref _music, value);
    }

    public Bitmap DefaultCover {
        get => _defaultCover;
        set => this.RaiseAndSetIfChanged(ref _defaultCover, value);
    }

}