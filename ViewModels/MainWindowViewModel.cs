using System;
using kon.Utils;
using ReactiveUI;

namespace kon.ViewModels;

public class MainWindowViewModel : ViewModelBase {

    private PlayBarViewModel playBarViewModel;


    public MainWindowViewModel() {
        PlayBarViewModel = new PlayBarViewModel();
        PlayBarViewModel.Music = CommonUtils.ParseToMusic("C:/Users/su/Desktop/放課後ティータイム - Listen!!.flac");

    }

    public PlayBarViewModel PlayBarViewModel {
        get => playBarViewModel;
        init => this.RaiseAndSetIfChanged(ref playBarViewModel, value);
    }

}