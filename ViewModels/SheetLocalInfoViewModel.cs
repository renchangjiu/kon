using System;
using System.Collections.ObjectModel;
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

    private ObservableCollection<Music> _musics = new();

    public SheetLocalInfoViewModel(LocalMusicSearcher searcher, DatabaseHandler db) {
        this.searcher = searcher;
        this.db = db;
        
        Musics.AddRange(this.db.listMusic(CC.LocalSheetId));
        
        this.searcher.OnStarted += () => {
            // TODO
        };
        this.searcher.OnFindNew += (sender, music) => {
            Dispatcher.UIThread.Post(() => {
                Musics.Add(music);
            });
        };
        this.searcher.start();
    }

    public ObservableCollection<Music> Musics {
        get => _musics;
        set => this.RaiseAndSetIfChanged(ref _musics, value);
    }

}