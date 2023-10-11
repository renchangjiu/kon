using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAvalonia.Core;
using kon.Models;
using kon.Utils;
using NLog;

namespace kon.Components;

public class LocalMusicSearcher {

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public event EventHandler<Music>? OnFindNew;

    public event Action? OnStarted;

    public event Action? OnFinished;

    private DatabaseHandler db;

    private readonly List<DirectoryInfo> roots;

    // 在歌单中已存在的歌曲
    private readonly HashSet<string> existMusicPaths;

    public LocalMusicSearcher(DatabaseHandler db) {
        this.db = db;
        roots = getRootDirectories();
        existMusicPaths = this.db.listMusicPaths(CC.LocalSheetId);
    }

    public void start() {
        OnStarted?.Invoke();
        Task.Run(() => {
            Log.Debug("LocalMusicSearcher started...");

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

                    OnFindNew?.Invoke(this, m);
                }
            }
        }
    }

    private static readonly HashSet<string> Exts = new(new[] {
        ".mp3",
        ".flac",
    });


    private static List<DirectoryInfo> getRootDirectories() {
        List<DirectoryInfo> res = new();
        // TODO
        res.Add(new DirectoryInfo("C:/Users/su/Desktop/"));
        res.Add(new DirectoryInfo("D:/download/[Airota&Nekomoe kissaten&VCB-Studio] Yagate Kimi ni Naru [Ma10p_1080p]"));
        res.Add(new DirectoryInfo("C:/Users/su/Music"));
        res.Add(new DirectoryInfo("D:/CloudMusic"));
        return res;
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

}