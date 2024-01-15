using System.Text.Encodings.Web;
using System.Text.Json;

namespace kon.Utils;

public static class CC {

    public static readonly JsonSerializerOptions JsonSerializerOptions = new() {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };


    /// <summary>
    /// 默认歌单(即本地歌单)ID
    /// </summary>
    public const int LocalSheetId = -1;


    public const string PerformerSep = " | ";

    public const string CK_SETTINGS = "settings";
    public const string CK_PLAYLIST = "playlist";

}