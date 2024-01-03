using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using Avalonia.Media.Imaging;
using kon.Models;
using kon.Utils;
using SQLite;

namespace kon;

public class Tests {

    public static void test1() {
        FileSystemWatcher w = new();
        w.Path = "C:/Users/su/Desktop";
        w.IncludeSubdirectories = true;
        w.Created += (sender, args) => {
            string path = args.FullPath;
            Console.WriteLine("Created: " + path);
        };
        w.Changed += (sender, args) => {
            string path = args.FullPath;
            Console.WriteLine("Changed: " + path);
        };
        w.Renamed += (sender, args) => {
            string path = args.FullPath;
            Console.WriteLine("Renamed: " + path);
        };
        w.Deleted += (sender, args) => {
            string path = args.FullPath;
            Console.WriteLine("Deleted: " + path);
        };
        w.EnableRaisingEvents = true;
        Console.WriteLine("started");
        Thread.CurrentThread.Join();
    }

    public static void testSqlite() {
        // string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.db");
        // SQLiteConnection db = new(path);
        //
        // db.CreateTable<Music>();
        string[] arr = { "\"a", "b" };
    }


    public static void testTag() {
        Bitmap? bitmap = CommonUtils.ParseCover("D:/CloudMusic/亜咲花 - Seize The Day.mp3", 32);
        Console.WriteLine("");
    }
}