using System;
using kon.Models;
using ReactiveUI;

namespace kon.ViewModels;

public class SheetInfoViewModel : ViewModelBase {

    private Sheet _sheet;

    public SheetInfoViewModel() {
        this.WhenAnyValue(model => model.Sheet)
            .Subscribe(sheet => {

            });
    }

    public Sheet Sheet {
        get => _sheet;
        set => this.RaiseAndSetIfChanged(ref _sheet, value);
    }

}