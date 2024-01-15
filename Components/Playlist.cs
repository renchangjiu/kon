using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using kon.Enums;
using kon.Models;
using kon.Utils;

namespace kon.Components;

public class Playlist {

    public event EventHandler<Music>? OnCurrentMusicChanged;

    public event EventHandler<List<Music>>? OnContentChanged;

    public event EventHandler<PlayMode>? OnPlayModeChanged;


    private readonly DatabaseHandler db;


    private readonly PlaylistInfo info;

    public Playlist(DatabaseHandler db) {
        this.db = db;
        info = load();

        raiseContentChangedEvent();
    }


    public void replace(List<Music> ms, int idx) {
        info.Musics = ms;
        Index = idx;
        raiseContentChangedEvent();
    }

    public bool isEmpty() {
        return Musics.Count == 0;
    }

    public bool isNotEmpty() {
        return !isEmpty();
    }

    public void addMusic(Music m) {
        Musics.Add(m);
        if (Index < 0 || size() == 1) {
            Index = 0;
        }

        raiseContentChangedEvent();
    }

    public void insertMusic(Music m, int idx) {
        Musics.Insert(idx, m);
        raiseContentChangedEvent();
    }

    public int size() {
        return Musics.Count;
    }

    public Music GetCurrentMusic() {
        if (Musics.Count == 0) {
            return null;
        }

        return Musics[Index];
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

    private int Index {
        get => info.Index;
        set {
            info.Index = value;
            OnCurrentMusicChanged?.Invoke(this, GetCurrentMusic());
        }
    }

    public PlayMode Mode {
        get => info.Mode;
        set {
            info.Mode = value;
            OnPlayModeChanged?.Invoke(this, info.Mode);
        }
    }

    public List<Music> Musics {
        get => info.Musics;
        set => info.Musics = value;
    }

    public void toNextMode() {
        int val = ((int)info.Mode);
        int len = Enum.GetValues(typeof(PlayMode)).Length;
        if (val == len - 1) {
            val = 0;
        } else {
            val++;
        }

        PlayMode next = ((PlayMode)val);
        Mode = next;
    }

    private void save() {
        db.updateConfig(CC.CK_PLAYLIST, info);
    }

    public PlaylistInfo load() {
        string value = db.selectConfig(CC.CK_PLAYLIST);
        if (string.IsNullOrWhiteSpace(value)) {
            return new PlaylistInfo();
        }

        return JsonSerializer.Deserialize<PlaylistInfo>(value)!;
    }

    private void raiseContentChangedEvent() {
        OnContentChanged?.Invoke(this, info.Musics);
        save();
    }

    public void clear() {
        info.Musics = [];
        Index = -1;
        raiseContentChangedEvent();
    }

}