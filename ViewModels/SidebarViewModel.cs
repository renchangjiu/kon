using System;
using System.Collections.Generic;
using FluentAvalonia.UI.Controls;
using kon.Components;
using kon.Models;
using NLog;
using ReactiveUI;

namespace kon.ViewModels;

public class SidebarViewModel : ViewModelBase {

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();


    private List<Sheet> sheets;

    private int _fixedSelectedIndex;
    private Sheet? _sheetSelectedItem;

    private DatabaseHandler db;

    public SidebarViewModel(DatabaseHandler db) {
        this.db = db;
        sheets = this.db.listSheet();
        MainContentViewModel mc = App.getService<MainContentViewModel>();
        this.WhenAnyValue(model => model.FixedSelectedIndex)
            .Subscribe(idx => {
                switch (idx) {
                    case 0:
                        mc.switchPage(typeof(SheetLocalInfoViewModel));
                        break;
                    case 1:
                        mc.switchPage(typeof(SheetRecentInfoViewModel));
                        break;
                }
            });
        this.WhenAnyValue(model => model.SheetSelectedItem)
            .WhereNotNull()
            .Subscribe(sheet => {
                SheetInfoViewModel tvm = App.getService<SheetInfoViewModel>();
                tvm.Sheet = sheet;
                mc.switchPage(typeof(SheetInfoViewModel));
            });
    }

    public List<Sheet> Sheets {
        get => sheets;
        set => this.RaiseAndSetIfChanged(ref sheets, value);
    }

    public int FixedSelectedIndex {
        get => _fixedSelectedIndex;
        set => this.RaiseAndSetIfChanged(ref _fixedSelectedIndex, value);
    }

    public Sheet? SheetSelectedItem {
        get => _sheetSelectedItem;
        set => this.RaiseAndSetIfChanged(ref _sheetSelectedItem, value);
    }

    public async void executeOpenCommand() {
        ContentDialog dialog = SheetAddViewModel.CreateDialog();
        ContentDialogResult ret = await dialog.ShowAsync();
        if (ret == ContentDialogResult.Primary) {
            flushSheets();
        }
    }

    public void doDeleteSheet(int sheetId) {
        db.deleteSheet(sheetId);
        flushSheets();
    }

    private void flushSheets() {
        Sheets = db.listSheet();
    }

}