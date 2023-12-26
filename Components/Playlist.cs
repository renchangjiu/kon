using System;
using System.Collections.Generic;
using kon.Enums;
using kon.Models;

namespace kon.Components;

public class Playlist {

    public event EventHandler<Music>? OnCurrentMusicChanged;

    public event EventHandler<List<Music>>? OnContentChanged;

    public event EventHandler<PlayMode>? OnPlayModeChanged;


    private List<Music> musics;

    private int _index;

    private PlayMode _mode;

    public Playlist() {
        musics = new List<Music>();
    }


    public void replace(List<Music> ms, int idx) {
        musics = ms;
        Index = idx;
        raiseEvent();
    }

    public bool isEmpty() {
        return musics.Count == 0;
    }

    public void addMusic(Music m) {
        musics.Add(m);
        if (Index < 0 || size() == 1) {
            Index = 0;
        }

        raiseEvent();
    }

    public void insertMusic(Music m, int idx) {
        musics.Insert(idx, m);
        raiseEvent();
    }

    public int size() {
        return musics.Count;
    }

    public Music getCurrentMusic() {
        return musics[Index];
    }


    public void next() {
        if (Index == size() - 1) {
            Index = 0;
        } else {
            Index += 1;
        }
    }

    public void prev() {
        if (Index == 0) {
            Index = size() - 1;
        } else {
            Index -= 1;
        }
    }

    public int Index {
        get => _index;
        set {
            _index = value;
            OnCurrentMusicChanged?.Invoke(this, getCurrentMusic());
        }
    }

    public PlayMode Mode {
        get => _mode;
        set {
            _mode = value;
            OnPlayModeChanged?.Invoke(this, _mode);
        }
    }

    private void raiseEvent() {
        OnContentChanged?.Invoke(this, musics);
    }

}