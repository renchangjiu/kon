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

    private readonly LocalMusicSearcher searcher;

    private readonly Settings settings;

    public ChooseMusicDirViewModel(Settings settings, LocalMusicSearcher searcher) {
        this.settings = settings;
        this.searcher = searcher;
        flushDirs();
    }

    /// <summary>
    /// only for designer preview
    /// </summary>
    public ChooseMusicDirViewModel(LocalMusicSearcher searcher) {
        this.searcher = searcher;
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
        List<string> olds = settings.Setting.MusicDirs.ToList();
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

        settings.Setting.MusicDirs = list;
        settings.save();
        searcher.start();
    }

}

public class CheckableMusicDir {

    public bool IsChecked { get; set; }

    public string Path { get; set; }

}