using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Threading;
using DynamicData;
using kon.Components;
using kon.Models;
using kon.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace kon.ViewModels;

public class SheetLocalInfoViewModel : ViewModelBase {

    private readonly LocalMusicSearcher searcher;

    private readonly DatabaseHandler db;

    private readonly Playlist playlist;


    [Reactive]
    public ObservableCollection<Music> Musics { get; set; } = new();

    [Reactive]
    public Music? FoundedMusic { get; set; }

    public SheetLocalInfoViewModel(LocalMusicSearcher searcher, DatabaseHandler db, Playlist playlist) {
        this.searcher = searcher;
        this.db = db;
        this.playlist = playlist;

        Musics.AddRange(this.db.listMusic(CC.LocalSheetId));
        for (int i = 0; i < Musics.Count; i++) {
            Musics[i].Index = i;
        }

        this.searcher.OnFindNew += (sender, m) => {
            Dispatcher.UIThread.Post(() => {
                FoundedMusic = m;
                m.Index = Musics.Count;
                Musics.Add(m);
            });
        };
        this.searcher.OnFinished += () => {
            Dispatcher.UIThread.Post(() => {
                FoundedMusic = null;
            });
        };
        this.searcher.start();
    }

    /// <summary>
    /// only for designer preview
    /// </summary>
    public SheetLocalInfoViewModel() {
    }


    public void OnReplacePlaylist(int idx) {
        playlist.replace(Musics.ToList(), idx);
    }

}