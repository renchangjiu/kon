using System;
using FluentAvalonia.UI.Controls;
using kon.Components;
using kon.Views;
using ReactiveUI;

namespace kon.ViewModels;

public class SheetAddViewModel : ViewModelBase {

    private string? _name;

    private ContentDialog dialog;

    public SheetAddViewModel(ContentDialog cd) {
        dialog = cd;
        dialog.PrimaryButtonClick += (sender, args) => {
            DatabaseHandler db = App.getService<DatabaseHandler>();
            db.addSheet(Name!);
        };
        this.WhenAnyValue(model => model.Name)
            .Subscribe(s => { dialog.IsPrimaryButtonEnabled = !string.IsNullOrWhiteSpace(s); });
    }

    public string? Name {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public static ContentDialog CreateDialog() {
        ContentDialog dialog = new() {
            Title = "新建歌单",
            PrimaryButtonText = "创建",
            CloseButtonText = "取消",
        };
        SheetAddViewModel vm = new(dialog);

        dialog.Content = new SheetAddView {
            DataContext = vm
        };
        return dialog;
    }

}