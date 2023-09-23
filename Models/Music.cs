using Avalonia.Media.Imaging;

namespace kon.Models;

public class Music {

    /// <summary>
    /// 歌曲ID
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 所属歌单ID
    /// </summary>
    public string Mid { get; set; }

    public string Path { get; set; }

    /// <summary>
    /// 文件大小, 字节
    /// </summary>
    public long Size { get; set; }
    public Bitmap? Cover { get; set; }
    public string Title { get; set; }
    public string[] Performers { get; set; }
    public string Album { get; set; }
    /// <summary>
    /// 时长, 秒
    /// </summary>
    public int Duration { get; set; }
    public string? DurationFormatted { get; set; }

}