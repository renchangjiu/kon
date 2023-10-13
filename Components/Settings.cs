using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using kon.Utils;
using NLog;

namespace kon.Components;

public class Settings {

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public ConfigC Config { get; private set; }

    private readonly FileInfo dest;

    public Settings() {
        dest = new FileInfo(Path.Combine(App.getDataPath(), "settings.json"));
        if (!dest.Exists) {
            dest.Create();
        }

        load();
        autoSave();
    }

    private void autoSave() {
        Task task = new(() => {
            save();
            Log.Debug("auto save settings");
            Task.Delay(TimeSpan.FromMinutes(2)).Wait();
        }, TaskCreationOptions.LongRunning);

        task.Start();
    }

    public void save() {
        string json = JsonSerializer.Serialize(Config, CC.JsonSerializerOptions);
        File.WriteAllText(dest.FullName, json, Encoding.UTF8);
    }


    private void load() {
        string json = File.ReadAllText(dest.FullName, Encoding.UTF8);
        if (string.IsNullOrWhiteSpace(json)) {
            Config = new ConfigC();
            Config.MusicDirs = LocalMusicSearcher.guessMusicDirs();
            return;
        }

        Config = JsonSerializer.Deserialize<ConfigC>(json)!;
    }


    public class ConfigC {

        public string Theme { get; set; }

        public string Version { get; set; }

        public PlayInfoC PlayInfo { get; set; } = new();

        public List<string> MusicDirs { get; set; } = new();

    }

    public class PlayInfoC {

        public int Index { get; set; }

        public double Duration { get; set; }

    }

}