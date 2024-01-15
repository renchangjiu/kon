using System.Collections.Generic;

namespace kon.Models;

public class SettingInfo {

    public string Theme { get; set; }

    public string Version { get; set; }

    public List<string> MusicDirs { get; set; } = new();

}

