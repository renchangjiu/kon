using SQLite;

namespace kon.Models;

[Table(nameof(Config))]
public class Config {

    [PrimaryKey]
    public string ConfigKey { get; set; }


    public string ConfigValue { get; set; }

}