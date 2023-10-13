using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DynamicData;
using kon.Components;
using ReactiveUI.Fody.Helpers;

namespace kon.ViewModels;

public class ChooseMusicDirViewModel : ViewModelBase {

    [Reactive]
    public ObservableCollection<CheckableMusicDir> MusicDirs { get; set; } = new();

    private readonly Settings settings;

    public ChooseMusicDirViewModel(Settings settings) {
        this.settings = settings;
        flushDirs();
    }

    /// <summary>
    /// only for designer preview
    /// </summary>
    public ChooseMusicDirViewModel() {
        MusicDirs.Add(new CheckableMusicDir() {
            IsChecked = true,
            Path = "c:/a/b/c"
        });
        MusicDirs.Add(new CheckableMusicDir() {
            IsChecked = false,
            Path = "D:/a/中文/c"
        });
    }

    public void flushDirs() {
        MusicDirs.Clear();
        List<string> olds = settings.Config.MusicDirs.ToList();
        foreach (var p in olds) {
            MusicDirs.Add(new CheckableMusicDir() {
                IsChecked = true,
                Path = p,
            });
        }
    }

    public void addDir(string path) {
        bool any = MusicDirs.Any(dir => dir.Path == path);
        if (!any) {
            MusicDirs.Add(new CheckableMusicDir() {
                IsChecked = true,
                Path = path,
            });
        }
    }

    public void save() {
        List<string> list = MusicDirs.ToList()
            .Where(v => v.IsChecked)
            .Select(v => v.Path)
            .ToList();

        settings.Config.MusicDirs = list;
        settings.save();
    }

}

public class CheckableMusicDir {

    public bool IsChecked { get; set; }

    public string Path { get; set; }

}