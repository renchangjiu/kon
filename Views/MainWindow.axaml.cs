using FluentAvalonia.UI.Windowing;

namespace kon.Views;

public partial class MainWindow : AppWindow {

    public MainWindow() {
        InitializeComponent();
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.Height = 0;

    }

}