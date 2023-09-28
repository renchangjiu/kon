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


    public MainContentViewModel(SheetLocalInfoViewModel sheetLocalInfoViewModel,
        SheetRecentInfoViewModel sheetRecentInfoViewModel, SheetInfoViewModel sheetInfoViewModel) {
        this.sheetLocalInfoViewModel = sheetLocalInfoViewModel;
        this.sheetRecentInfoViewModel = sheetRecentInfoViewModel;
        this.sheetInfoViewModel = sheetInfoViewModel;

        CurrentPage = sheetLocalInfoViewModel;
    }

    public void switchPage(Type type) {
        ViewModelBase? vm = App.ServiceProvider.GetRequiredService(type) as ViewModelBase;
        CurrentPage = vm;
    }

    public ViewModelBase CurrentPage {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

}