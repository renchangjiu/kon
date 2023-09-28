using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Animation;
using kon.Models;
using NAudio.Wave;
using NLog;

namespace kon.Components;

public class Player {

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    public event EventHandler<int>? OnPositionChanged;
    public event Action<int>? OnVolumeChanged;
    public event EventHandler<PlayState>? OnStateChanged;

    private int volume;
    private bool muted;

    private Music? m;

    private WaveOutEvent device;
    private AudioFileReader? audio;

    public Player() {
        Task.Run(() => {
            while (true) {
                Log.Trace("trace pos");
                if (audio == null) {
                    Thread.Sleep(1000);
                    continue;
                }

                int pos = ((int)audio.CurrentTime.TotalSeconds);
                OnPositionChanged?.Invoke(this, pos);
                Thread.Sleep(1000);
            }
        });
    }

    public void init(Music music) {
        m = music;
        device = new();
        audio = new(m.Path);
        device.Init(audio);
        State = PlayState.Pause;
    }


    public void reset() {
        device?.Dispose();
        audio?.Dispose();
        State = PlayState.Stop;
    }

    /// <summary>
    /// 开始或继续播放。
    /// 如果以前已暂停播放，则将从暂停的位置继续播放。
    /// 如果播放已停止或之前从未开始过，则播放将从头开始。
    /// </summary>
    public void play(Music music) {
        if (isStopped() || m == null || m.Path != music.Path) {
            stop();
            audio = new AudioFileReader(music.Path);
            device = new WaveOutEvent();
            device.Init(audio);
        }

        m = music;
        if (isPlaying()) {
            pause();
        } else {
            play();
        }
    }


    public void pause() {
        device.Pause();
        State = PlayState.Pause;
        OnStateChanged?.Invoke(this, State);
    }

    private void play() {
        device.Play();
        State = PlayState.Run;
        OnStateChanged?.Invoke(this, State);
    }

    public void stop() {
        device?.Stop();
        device?.Dispose();
        audio?.Dispose();
        State = PlayState.Stop;
        OnStateChanged?.Invoke(this, State);
    }

    public int Volume {
        get => volume;
        set => volume = value;
    }

    public bool Muted {
        get => muted;
        set => muted = value;
    }

    public int Position {
        get {
            if (audio == null) {
                return 0;
            }

            return (int)audio.CurrentTime.TotalSeconds;
        }
        set => audio.CurrentTime = TimeSpan.FromSeconds(value);
    }

    public bool isPlaying() {
        return State == PlayState.Run;
    }

    public bool isStopped() {
        return State == PlayState.Stop;
    }

    public PlayState State { get; private set; }

}