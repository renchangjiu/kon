using System;
using System.IO;
using System.Threading;
using kon.Enums;

namespace kon;

public class Tests {

    public static void test1() {
        FileSystemWatcher w = new();
        w.Path = "C:/Users/douziqiang/Desktop";
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


    public static void test() {
        Array values = Enum.GetValues(typeof(PlayMode));
        int valuesLength = values.Length;
        Console.WriteLine("");
    }
}