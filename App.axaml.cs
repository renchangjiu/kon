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
        NLogConfig.init();
        buildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow {
                DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static void buildServiceProvider() {
        ServiceCollection collection = new();
        collection.AddSingleton<Player>();
        collection.AddSingleton<Playlist>();
        collection.AddSingleton<PlayBarViewModel>();
        collection.AddSingleton<MainWindowViewModel>();


        ServiceProvider = collection.BuildServiceProvider();
    }

}