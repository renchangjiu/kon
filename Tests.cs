using System;
using System.IO;
using System.Text.Json;
using kon.Models;
using kon.Utils;
using SQLite;

namespace kon;

public class Tests {

    public static void test1() {
        string formatDuration = CommonUtils.formatDuration(150);
        Console.WriteLine("");
    }

    public static void testSqlite() {
        // string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.db");
        // SQLiteConnection db = new(path);
        //
        // db.CreateTable<Music>();
        string[] arr = { "\"a", "b" };
    }
}