using System.Collections.Generic;
using kon.Enums;

namespace kon.Models;

public class PlaylistInfo {

    public List<Music> Musics { get; set; }

    public PlayMode Mode { get; set; }

    public int Index { get; set; }

    public int Position { get; set; }

}