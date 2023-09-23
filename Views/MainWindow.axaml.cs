using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;

namespace kon.Views;

public partial class MainWindow : Window {

    public MainWindow() {
        InitializeComponent();
        IControlTemplate? mySliderTemplate = MyPlayBarView.MySlider.Template;
        string? s = mySliderTemplate.ToString();
        Console.WriteLine("");
    }

}