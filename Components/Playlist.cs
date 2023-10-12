using System;
using System.Collections.Generic;
using kon.Enums;
using kon.Models;

namespace kon.Components;

public class Playlist {

    public PlayMode Mode { get; set; } = PlayMode.Loop;

    public event EventHandler<Music> OnCurrentMusicChanged;

    public event EventHandler<List<Music>> OnContentChanged;

    private int index = -1;

    private List<Music> musics;


    public Playlist() {
        musics = new List<Music>();
    }

    public void replace(List<Music> ms, int idx) {
        musics = ms;
        setIndex(idx);
        raiseEvent();
    }

    public bool isEmpty() {
        return musics.Count == 0;
    }

    public void addMusic(Music m) {
        musics.Add(m);
        if (index < 0 || size() == 1) {
            setIndex(0);
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
        return musics[index];
    }

    public void setIndex(int idx) {
        // if (index != idx) {
        index = idx;
        OnCurrentMusicChanged?.Invoke(this, getCurrentMusic());
        // }
    }

    private void raiseEvent() {
        OnContentChanged?.Invoke(this, musics);
    }
}