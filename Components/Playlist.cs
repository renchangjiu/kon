using System.Collections.Generic;
using kon.Enums;
using kon.Models;

namespace kon.Components;

public class Playlist {

    public PlayMode Mode { get; set; } = PlayMode.Loop;

    public delegate void CurrentMusicChangedHandler(Music m);

    public event CurrentMusicChangedHandler CurrentMusicChanged;

    private int index = -1;

    private List<Music> musics;

    private Player player;

    public Playlist(Player player) {
        this.player = player;
        musics = new List<Music>();
    }

    public bool isEmpty() {
        return musics.Count == 0;
    }

    public void addMusic(Music m) {
        musics.Add(m);
        if (index < 0) {
            setIndex(0);
        }
    }

    public void insertMusic(Music m, int idx) {
        musics.Insert(idx, m);
    }

    public int size() {
        return musics.Count;
    }

    public Music getCurrentMusic() {
        return musics[index];
    }

    public void setIndex(int idx) {
        if (index != idx) {
            index = idx;
            CurrentMusicChanged?.Invoke(getCurrentMusic());
        }
    }

}