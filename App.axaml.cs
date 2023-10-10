using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using kon.Components;
using kon.Configs;
using kon.ViewModels;
using kon.Views;
using Microsoft.Extensions.DependencyInjection;

namespace kon;

public partial class App : Application {

    private static ServiceProvider _serviceProvider;

    public static ServiceProvider ServiceProvider {
        get => _serviceProvider;
        private set => _serviceProvider = value;
    }

    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        init();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow {
                DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>()
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
        if (!Path.Exists(getDataPath())) {
            Directory.CreateDirectory(getDataPath());
        }
    }

    public static void buildServiceProvider() {
        ServiceCollection co = new();

        co.AddSingleton<DatabaseHandler>();
        co.AddSingleton<Player>();
        co.AddSingleton<Playlist>();

        co.AddSingleton<PlayBarViewModel>();
        co.AddSingleton<SidebarViewModel>();
        co.AddSingleton<SheetLocalInfoViewModel>();
        co.AddSingleton<SheetRecentInfoViewModel>();
        co.AddSingleton<SheetInfoViewModel>();
        // co.AddSingleton<SheetAddViewModel>();

        co.AddSingleton<MainContentViewModel>();
        co.AddSingleton<MainWindowViewModel>();


        ServiceProvider = co.BuildServiceProvider();
    }

    public static string getDataPath() {
        return Path.Combine(Environment.CurrentDirectory, "data");
    }

}