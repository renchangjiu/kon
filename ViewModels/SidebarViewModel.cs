using System;
using System.Collections.Generic;
using kon.Models;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using ReactiveUI;

namespace kon.ViewModels;

public class SidebarViewModel : ViewModelBase {

    private List<Sheet> sheets;

    private int _fixedSelectedIndex;

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();


    public SidebarViewModel() {
        sheets = new List<Sheet>() {
            new Sheet() {
                Name = "摇曳露营"
            },
        };
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

}