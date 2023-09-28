using System;
using System.Collections.Generic;
using SQLite;

namespace kon.Models;

/// <summary>
/// 歌单
/// </summary>
[Table(nameof(Sheet))]
public class Sheet {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime Created { get; set; }

    [Ignore]
    public string CreatedStr { get; set; }

    public int PlayCount { get; set; }


    public List<Music> musics;

}