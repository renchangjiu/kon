namespace kon.ViewModels;

public class PlayPageViewModel : ViewModelBase {

    public void HandleToHomepage() {
        App.getService<MainWindowViewModel>().SwitchToHomepage();
    }

}