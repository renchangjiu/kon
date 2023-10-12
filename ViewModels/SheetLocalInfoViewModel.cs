using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Threading;
using DynamicData;
using kon.Components;
using kon.Models;
using kon.Utils;
using ReactiveUI;

namespace kon.ViewModels;

public class SheetLocalInfoViewModel : ViewModelBase {

    private readonly LocalMusicSearcher searcher;

    private readonly DatabaseHandler db;

    private readonly Playlist playlist;


    private ObservableCollection<Music> _musics = new();


    public SheetLocalInfoViewModel(LocalMusicSearcher searcher, DatabaseHandler db, Playlist playlist) {
        this.searcher = searcher;
        this.db = db;
        this.playlist = playlist;

        Musics.AddRange(this.db.listMusic(CC.LocalSheetId));
        for (int i = 0; i < Musics.Count; i++) {
            Musics[i].Index = i;
        }

        this.searcher.OnStarted += () => {
            // TODO
        };
        this.searcher.OnFindNew += (sender, music) => {
            Dispatcher.UIThread.Post(() => {
                music.Index = Musics.Count;
                Musics.Add(music);
            });
        };
        this.searcher.start();
    }

    public ObservableCollection<Music> Musics {
        get => _musics;
        set => this.RaiseAndSetIfChanged(ref _musics, value);
    }

    public void OnReplacePlaylist(int idx) {
        playlist.replace(Musics.ToList(), idx);
    }

}