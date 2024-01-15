using System;
using System.Text.Json;
using System.Threading.Tasks;
using kon.Models;
using kon.Utils;
using NLog;

namespace kon.Components;

public class Settings {

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    private readonly DatabaseHandler db;

    public SettingInfo Setting { get; private set; }


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
        db.updateConfig(CC.CK_SETTINGS, Setting);
    }


    private void load() {
        string value = db.selectConfig(CC.CK_SETTINGS);
        if (string.IsNullOrWhiteSpace(value)) {
            Setting = new SettingInfo();
            Setting.MusicDirs = LocalMusicSearcher.guessMusicDirs();
            save();
            return;
        }

        Setting = JsonSerializer.Deserialize<SettingInfo>(value)!;
    }

}