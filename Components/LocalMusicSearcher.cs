using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using kon.Models;
using kon.Utils;
using NLog;

namespace kon.Components;

/// <summary>
/// TODO: file watch
/// </summary>
public class LocalMusicSearcher {

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public event EventHandler<Music>? OnFindNew;

    public event Action? OnStarted;

    public event Action? OnFinished;

    private readonly DatabaseHandler db;

    private readonly Settings settings;


    // 在歌单中已存在的歌曲
    private readonly HashSet<string> existMusicPaths;

    public LocalMusicSearcher(DatabaseHandler db, Settings settings) {
        this.db = db;
        this.settings = settings;
        existMusicPaths = this.db.listMusicPaths(CC.LocalSheetId);
    }

    public void start() {
        OnStarted?.Invoke();
        Task.Run(() => {
            Log.Debug("LocalMusicSearcher started...");
            List<DirectoryInfo> roots = getRootDirectories();
            foreach (var root in roots) {
                search(root);
            }

            stop();
        });
    }

    public void stop() {
        Log.Debug("LocalMusicSearcher stopped.");
        OnFinished?.Invoke();
    }

    private void search(DirectoryInfo dir) {
        if (!dir.Exists) {
            return;
        }

        List<DirectoryInfo> dirs = new();
        dirs.Add(dir);
        while (dirs.Any()) {
            DirectoryInfo first = dirs[0];
            // Log.Debug(first.FullName);
            dirs.RemoveAt(0);
            processFiles(getFiles(first));
            dirs.AddRange(GetDirectories(first));
        }
    }

    private void processFiles(IEnumerable<FileInfo> files) {
        foreach (var file in files) {
            // Log.Debug(file.FullName);
            string ext = file.Extension;
            if (Exts.Contains(ext)) {
                string path = file.FullName;
                if (!existMusicPaths.Contains(path)) {
                    Log.Debug("find new music: " + path);
                    existMusicPaths.Add(path);

                    Music m = CommonUtils.ParseToMusic(path);
                    m.Mid = CC.LocalSheetId;

                    db.addMusic(m);
                    // Thread.Sleep(1000);
                    OnFindNew?.Invoke(this, m);
                }
            }
        }
    }

    private static readonly HashSet<string> Exts = new(new[] {
        ".mp3",
        ".flac",
    });


    private List<DirectoryInfo> getRootDirectories() {
        List<string> paths = settings.Config.MusicDirs;
        List<DirectoryInfo> dirs = paths.Select(p => new DirectoryInfo(p)).ToList();
        return dirs;
    }


    private void watchDirs() {

    }

    private static FileInfo[] getFiles(DirectoryInfo dir) {
        try {
            return dir.GetFiles();
        } catch (UnauthorizedAccessException e) {
            return Array.Empty<FileInfo>();
        }
    }

    private static DirectoryInfo[] GetDirectories(DirectoryInfo dir) {
        try {
            return dir.GetDirectories();
        } catch (UnauthorizedAccessException e) {
            return Array.Empty<DirectoryInfo>();
        }
    }

    public static List<string> guessMusicDirs() {
        List<string> res = new();

        res.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));

        return res;
    }

}