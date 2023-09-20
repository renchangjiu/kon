using System;
using System.IO;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using kon.Models;
using kon.Utils;
using ReactiveUI;

namespace kon.ViewModels;

public class PlayBarViewModel : ViewModelBase {

    private Music? _music;

    private Bitmap _defaultCover;

    public PlayBarViewModel() {
        this.WhenAnyValue(model => model.Music)
            .Subscribe(o => {
                if (Music == null) {
                    Music = CommonUtils.ParseToMusic("C:/Users/su/Desktop/放課後ティータイム - Listen!!.flac");
                }
            });
        Stream fs = AssetLoader.Open(new Uri("avares://kon/Assets/Icons/default_music_image.png"));
        DefaultCover = new Bitmap(fs);
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