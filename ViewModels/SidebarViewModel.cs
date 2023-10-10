using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using kon.Components;
using kon.Models;
using kon.Views;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using ReactiveUI;
using Brushes = Avalonia.Media.Brushes;

namespace kon.ViewModels;

public class SidebarViewModel : ViewModelBase {

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();


    private List<Sheet> sheets;

    private int _fixedSelectedIndex;

    private DatabaseHandler db;

    public SidebarViewModel(DatabaseHandler db) {
        this.db = db;
        sheets = this.db.listSheet();
        this.WhenAnyValue(model => model.FixedSelectedIndex)
            .Subscribe(idx => {
                MainContentViewModel mc = App.getService<MainContentViewModel>();
                switch (idx) {
                    case 0:
                        mc.switchPage(typeof(SheetLocalInfoViewModel));
                        break;
                    case 1:
                        mc.switchPage(typeof(SheetRecentInfoViewModel));
                        break;
                }
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