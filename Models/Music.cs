using Avalonia.Media.Imaging;
using SQLite;

namespace kon.Models;

[Table(nameof(Music))]
public class Music {

    /// <summary>
    /// 歌曲ID
    /// </summary>
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>
    /// 所属歌单ID
    /// </summary>
    public int Mid { get; set; }

    public string Path { get; set; }

    /// <summary>
    /// 文件大小, 字节
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// 文件大小, like: 10MB
    /// </summary>
    public string SizeFormatted { get; set; }

    [Ignore]
    public Bitmap? Cover { get; set; }

    public string? Title { get; set; }

    /// <summary>
    /// 歌手, 多人间用 " | " 分隔
    /// </summary>
    public string? Performer { get; set; }

    public string? Album { get; set; }

    /// <summary>
    /// 时长, 秒
    /// </summary>
    public int Duration { get; set; }

    public string? DurationFormatted { get; set; }

    /// <summary>
    /// 在某歌单内的下标, base 0
    /// </summary>
    [Ignore]
    public int Index { get; set; }

    protected bool Equals(Music other) {
        return Path == other.Path;
    }

    /// <summary>
    /// check equals by "Path"
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj) {
        if (ReferenceEquals(null, obj)) {
            return false;
        }

        if (ReferenceEquals(this, obj)) {
            return true;
        }

        if (obj.GetType() != this.GetType()) {
            return false;
        }

        return Equals((Music)obj);
    }

    public override int GetHashCode() {
        return Path.GetHashCode();
    }

}