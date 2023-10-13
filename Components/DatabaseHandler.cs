using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using kon.Models;
using SQLite;

namespace kon.Components;

public class DatabaseHandler {

    private readonly SQLiteConnection db;

    public DatabaseHandler() {
        db = new SQLiteConnection(Path.Combine(App.getDataPath(), "data.db"));
        db.CreateTables<Music, Sheet>();
    }

    public void addMusic(Music m) {
        db.Insert(m);
    }

    public void updateMusic(Music m) {
        db.Update(m);
    }

    public List<Music> listMusic(int sheetId) {
        return db.Table<Music>()
            .Where(m => m.Mid == sheetId)
            .ToList();
    }

    public HashSet<string> listMusicPaths(int sheetId) {
        return db.Table<Music>()
            .Where(m => m.Mid == sheetId)
            .Select(m => m.Path)
            .ToHashSet();
    }

    public void deleteMusic(int id) {
        db.Delete(id, new TableMapping(typeof(Music)));
    }

    public void addSheet(string name) {
        Sheet sheet = new() {
            Name = name,
            Created = DateTime.Now,
            PlayCount = 0,
        };
        db.Insert(sheet);
    }

    public List<Sheet> listSheet() {
        TableQuery<Sheet> query = db.Table<Sheet>();
        return query.ToList();
    }

    public void deleteSheet(int id) {
        db.Delete(id, new TableMapping(typeof(Sheet)));
    }

}