using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using kon.Components;
using kon.Configs;
using kon.ViewModels;
using kon.Views;
using Microsoft.Extensions.DependencyInjection;

namespace kon;

public partial class App : Application {

    public static ServiceProvider ServiceProvider { get; private set; }

    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        init();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow {
                DataContext = getService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public static T getService<T>() where T : notnull {
        return ServiceProvider.GetRequiredService<T>();
    }

    public static void init() {
        NLogConfig.init();
        buildServiceProvider();
        string dataPath = getDataPath();
        if (!Directory.Exists(dataPath)) {
            Directory.CreateDirectory(dataPath);
        }
    }

    private static void buildServiceProvider() {
        ServiceCollection co = new();

        co.AddSingleton<DatabaseHandler>();
        co.AddSingleton<Player>();
        co.AddSingleton<Playlist>();
        co.AddSingleton<Settings>();

        co.AddSingleton<LocalMusicSearcher>();

        co.AddSingleton<ChooseMusicDirViewModel>();
        co.AddSingleton<PlayBarViewModel>();
        co.AddSingleton<PlaylistViewModel>();
        co.AddSingleton<SidebarViewModel>();
        co.AddSingleton<SheetLocalInfoViewModel>();
        co.AddSingleton<SheetRecentInfoViewModel>();
        co.AddSingleton<SheetInfoViewModel>();

        co.AddSingleton<MainContentViewModel>();
        co.AddSingleton<MainWindowViewModel>();
        co.AddSingleton<HomepageViewModel>();
        co.AddSingleton<PlayPageViewModel>();
        ServiceProvider = co.BuildServiceProvider();
    }

    public static string getDataPath() {
        return Path.Combine(Environment.CurrentDirectory, "data");
    }

    public static T FindResource<T>(object key) {
        return (T)Current!.FindResource(key)!;
    }

    public static Geometry FindGeometryResource(object key) {
        return FindResource<Geometry>(key);
    }

}