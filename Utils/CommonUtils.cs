using System;
using System.IO;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using kon.Models;
using TagLib;
using File = TagLib.File;

namespace kon.Utils;

public static class CommonUtils {

    public static Music ParseToMusic(string path) {
        File file = File.Create(path);
        Tag tag = file.Tag;
        Music m = new() {
            Path = path,
            Size = file.Length,
            Cover = ParseCover(file, 48),
            Title = tag.Title,
            Performers = tag.Performers,
            Album = tag.Album,
            Duration = (int)file.Properties.Duration.TotalSeconds
        };
        m.DurationFormatted = formatDuration(m.Duration);

        file.Dispose();
        return m;
    }


    private static Bitmap? ParseCover(File tagLibFile, int width) {
        IPicture[] pictures = tagLibFile.Tag.Pictures;
        if (pictures.Length == 0) {
            return null;
        }

        IPicture pic = pictures[0];
        byte[] bytes = pic.Data.Data;
        MemoryStream ms = new(bytes);
        return Bitmap.DecodeToWidth(ms, width);
    }

    /// <param name="path">like: /Assets/icon.ico</param>
    public static Bitmap getBitmapFromAsset(string path) {
        Stream fs = AssetLoader.Open(new Uri($"avares://kon{path}"));
        Bitmap ret = new(fs);
        fs.Dispose();
        return ret;
    }

    /// <summary>
    /// 把秒级时长格式化为mm:ss形式
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static string formatDuration(int duration) {
        string min = "00";
        string sec;
        if (duration < 60) {
            sec = duration.ToString().PadLeft(2, '0');
        } else {
            int minInt = duration / 60;
            int secInt = duration % 60;
            min = minInt.ToString().PadLeft(2, '0');
            sec = secInt.ToString().PadLeft(2, '0');
        }

        return min + ":" + sec;
    }

}