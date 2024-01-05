using ReactiveUI.Fody.Helpers;

namespace kon.ViewModels;

public class HomepageViewModel : ViewModelBase {

    [Reactive]
    public PlayBarViewModel PlayBarViewModel { get; set; }

    [Reactive]
    public SidebarViewModel SidebarViewModel { get; set; }

    [Reactive]
    public MainContentViewModel MainContentViewModel { get; set; }

    public HomepageViewModel(
        PlayBarViewModel playBarViewModel,
        SidebarViewModel sidebarViewModel,
        MainContentViewModel mainContentViewModel) {
        PlayBarViewModel = playBarViewModel;
        SidebarViewModel = sidebarViewModel;
        MainContentViewModel = mainContentViewModel;
    }

}