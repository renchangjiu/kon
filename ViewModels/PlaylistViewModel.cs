using System;
using System.Collections.Generic;
using kon.Components;
using kon.Models;
using ReactiveUI;

namespace kon.ViewModels;

public class PlaylistViewModel : ViewModelBase {

    private readonly Playlist playlist;

    private List<Music> _musics;

    public PlaylistViewModel(Playlist playlist) {
        this.playlist = playlist;
        this.playlist.OnContentChanged += (sender, ms) => {
            Musics = ms;
        };
    }

    public List<Music> Musics {
        get => _musics;
        set => this.RaiseAndSetIfChanged(ref _musics, value);
    }

}