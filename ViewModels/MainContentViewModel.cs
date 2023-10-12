using System;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace kon.ViewModels;

/// <summary>
/// 中间主要部分
/// </summary>
public class MainContentViewModel : ViewModelBase {

    private SheetLocalInfoViewModel sheetLocalInfoViewModel;

    private SheetRecentInfoViewModel sheetRecentInfoViewModel;

    private SheetInfoViewModel sheetInfoViewModel;

    private ViewModelBase _currentPage;

    private bool _isPaneOpen = false;

    public PlaylistViewModel PlaylistViewModel { get; }


    public MainContentViewModel(SheetLocalInfoViewModel sheetLocalInfoViewModel,
        SheetRecentInfoViewModel sheetRecentInfoViewModel,
        SheetInfoViewModel sheetInfoViewModel,
        PlaylistViewModel playlistViewModel) {
        this.sheetLocalInfoViewModel = sheetLocalInfoViewModel;
        this.sheetRecentInfoViewModel = sheetRecentInfoViewModel;
        this.sheetInfoViewModel = sheetInfoViewModel;
        PlaylistViewModel = playlistViewModel;

        CurrentPage = sheetLocalInfoViewModel;
    }

    public void switchPage(Type type) {
        ViewModelBase? vm = App.ServiceProvider.GetRequiredService(type) as ViewModelBase;
        CurrentPage = vm;
    }


    public void SwitchPlaylistViewVisible() {
        IsPaneOpen = !IsPaneOpen;
    }

    public ViewModelBase CurrentPage {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public bool IsPaneOpen {
        get => _isPaneOpen;
        set => this.RaiseAndSetIfChanged(ref _isPaneOpen, value);
    }

}