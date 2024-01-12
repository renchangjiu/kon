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
            Size = new FileInfo(path).Length,
            Title = tag.Title ?? Path.GetFileNameWithoutExtension(path),
            Performer = string.Join(CC.PerformerSep, tag.Performers),
            Album = tag.Album,
            Duration = (int)file.Properties.Duration.TotalSeconds
        };
        if (string.Empty == m.Performer) {
            m.Performer = null;
        }

        m.DurationFormatted = formatDuration(m.Duration);
        m.SizeFormatted = formatSize(m.Size);

        file.Dispose();
        return m;
    }


    public static Bitmap? ParseCover(string path, int width) {
        File file = File.Create(path);
        IPicture[] pictures = file.Tag.Pictures;
        if (pictures.Length == 0) {
            return null;
        }

        IPicture pic = pictures[0];
        byte[] bytes = pic.Data.Data;
        MemoryStream ms = new(bytes);

        // return new Bitmap(Bitmap.GetFactory().LoadBitmapToWidth(stream, width, interpolationMode));
        // return WriteableBitmap.DecodeToWidth(ms, width);

        return Bitmap.DecodeToWidth(ms, width);
        // return null;
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

    public static string formatSize(long size) {
        decimal d = size;
        decimal dr = d / 1024 / 1024;
        return dr.ToString("F1") + "MB";
    }

}