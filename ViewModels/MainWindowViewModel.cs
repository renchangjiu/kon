using System;
using kon.Utils;
using ReactiveUI;

namespace kon.ViewModels;

public class MainWindowViewModel : ViewModelBase {

    private PlayBarViewModel playBarViewModel;

    private SidebarViewModel sidebarViewModel;

    private MainContentViewModel mainContentViewModel;


    public MainWindowViewModel(PlayBarViewModel playBarViewModel, SidebarViewModel sidebarViewModel,
        MainContentViewModel mainContentViewModel) {
        this.playBarViewModel = playBarViewModel;
        this.sidebarViewModel = sidebarViewModel;
        this.mainContentViewModel = mainContentViewModel;
    }


    public PlayBarViewModel PlayBarViewModel {
        get => playBarViewModel;
        init => this.RaiseAndSetIfChanged(ref playBarViewModel, value);
    }

    public SidebarViewModel SidebarViewModel {
        get => sidebarViewModel;
        set => this.RaiseAndSetIfChanged(ref sidebarViewModel, value);
    }

    public MainContentViewModel MainContentViewModel {
        get => mainContentViewModel;
        set => this.RaiseAndSetIfChanged(ref mainContentViewModel, value);
    }

}