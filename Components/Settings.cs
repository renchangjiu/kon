using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using kon.Models;
using kon.Utils;
using NLog;

namespace kon.Components;

public class Settings {

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    private readonly DatabaseHandler db;

    public ConfigC Config { get; private set; }


    public Settings(DatabaseHandler db) {
        this.db = db;
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
        Config cfg = new Config() {
            ConfigKey = CC.CK_SETTINGS,
            ConfigValue = json
        };
        db.updateConfig(cfg);
    }


    private void load() {
        string value = db.selectConfig(CC.CK_SETTINGS);
        if (string.IsNullOrWhiteSpace(value)) {
            Config = new ConfigC();
            Config.MusicDirs = LocalMusicSearcher.guessMusicDirs();
            save();
            return;
        }

        Config = JsonSerializer.Deserialize<ConfigC>(value)!;
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