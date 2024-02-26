using ReactiveUI.Fody.Helpers;

namespace kon.ViewModels;

public class MainWindowViewModel : ViewModelBase {

    [Reactive]
    public ViewModelBase CurrentPage { get; set; }

    [Reactive]
    public bool IsTransitionReversed { get; set; }


    private readonly HomepageViewModel homepageViewModel;

    private readonly PlayPageViewModel playPageViewModel;


    public MainWindowViewModel(HomepageViewModel homepageViewModel, PlayPageViewModel playPageViewModel) {
        this.homepageViewModel = homepageViewModel;
        this.playPageViewModel = playPageViewModel;

        SwitchToHomepage();
    }

    public void SwitchToPlayPage() {
        CurrentPage = playPageViewModel;
        IsTransitionReversed = false;
    }

    public void SwitchToHomepage() {
        CurrentPage = homepageViewModel;
        IsTransitionReversed = true;
    }

}