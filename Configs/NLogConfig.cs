using System;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace kon.Configs;

public static class NLogConfig {

    public static void init() {
        string baseDir = Environment.CurrentDirectory;
        var config = new LoggingConfiguration();
        const string layout = "${longdate} | ${level:uppercase=true} | ${logger} | ${message:withexception=true}";

        var logConsole = new ConsoleTarget("logconsole") {
            Layout = layout,
        };

        var logfileInfo = new FileTarget("logfile") {
            FileName = $"{baseDir}/logs/info.log",
            Layout = layout,
            ArchiveFileName = $"{baseDir}/logs/info.{{#}}.log",
            ArchiveEvery = FileArchivePeriod.Day,
            ArchiveNumbering = ArchiveNumberingMode.Date,
            ArchiveDateFormat = "yyyyMMdd",
            MaxArchiveFiles = 10,
            ConcurrentWrites = true,
            KeepFileOpen = false,
            Encoding = Encoding.UTF8
        };
        var logfileError = new FileTarget("logfile") {
            FileName = $"{baseDir}/logs/error.log",
            Layout = layout,
            ArchiveFileName = $"{baseDir}/logs/error.{{#}}.log",
            ArchiveEvery = FileArchivePeriod.Day,
            ArchiveNumbering = ArchiveNumberingMode.Date,
            ArchiveDateFormat = "yyyyMMdd",
            MaxArchiveFiles = 100,
            ConcurrentWrites = true,
            KeepFileOpen = false,
            Encoding = Encoding.UTF8
        };


        config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
        config.AddRule(LogLevel.Debug, LogLevel.Warn, logfileInfo);
        config.AddRule(LogLevel.Error, LogLevel.Fatal, logfileError);

        LogManager.Configuration = config;
    }

}