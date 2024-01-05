using ReactiveUI.Fody.Helpers;

namespace kon.ViewModels;

public class MainWindowViewModel : ViewModelBase {

    [Reactive]
    public ViewModelBase CurrentPage { get; set; }


    private readonly HomepageViewModel homepageViewModel;

    private readonly PlayPageViewModel playPageViewModel;


    public MainWindowViewModel(HomepageViewModel homepageViewModel, PlayPageViewModel playPageViewModel) {
        this.homepageViewModel = homepageViewModel;
        this.playPageViewModel = playPageViewModel;

        CurrentPage = this.homepageViewModel;

    }

    public void SwitchToPlayPage() {
        CurrentPage = playPageViewModel;
    }

    public void SwitchToHomepage() {
        CurrentPage = homepageViewModel;
    }



}