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


    private List<Music> musics;

    private int _index;

    private PlayMode _mode;

    private readonly FileInfo dest;

    public Playlist() {
        dest = new FileInfo(Path.Combine(App.getDataPath(), "playlist.json"));
    }


    public void replace(List<Music> ms, int idx) {
        musics = ms;
        Index = idx;
        raiseContentChangedEvent();
    }

    public bool isEmpty() {
        return musics.Count == 0;
    }

    public void addMusic(Music m) {
        musics.Add(m);
        if (Index < 0 || size() == 1) {
            Index = 0;
        }

        raiseContentChangedEvent();
    }

    public void insertMusic(Music m, int idx) {
        musics.Insert(idx, m);
        raiseContentChangedEvent();
    }

    public int size() {
        return musics.Count;
    }

    public Music getCurrentMusic() {
        if (musics.Count == 0) {
            return null;
        }
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

    public void toNextMode() {
        int val = ((int)_mode);
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
        FileStream fs = dest.Open(FileMode.Create);
        JsonSerializer.SerializeAsync(fs, musics, CC.JsonSerializerOptions);
        fs.Close();
    }

    public void load() {
        if (!dest.Exists) {
            musics = [];
            return;
        }

        string json = File.ReadAllText(dest.FullName, Encoding.UTF8);
        if (string.IsNullOrWhiteSpace(json)) {
            musics = [];
            return;
        }

        musics = JsonSerializer.Deserialize<List<Music>>(json)!;

        // TODO: read settings
        Index = 0;
        Mode = PlayMode.Repeat;

        raiseContentChangedEvent();
    }

    private void raiseContentChangedEvent() {
        OnContentChanged?.Invoke(this, musics);
        save();
    }

    public void clear() {
        musics = [];
        Index = -1;
        raiseContentChangedEvent();
    }

}