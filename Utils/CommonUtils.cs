using System.IO;
using Avalonia.Media.Imaging;
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

}