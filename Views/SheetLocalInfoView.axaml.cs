using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Encodings.Web;
using System.Web;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using FluentAvalonia.UI.Controls;
using kon.Models;
using kon.Utils;
using kon.ViewModels;
using ReactiveUI;

namespace kon.Views;

public partial class SheetLocalInfoView : ReactiveUserControl<SheetLocalInfoViewModel> {

    public SheetLocalInfoView() {
        InitializeComponent();
    }

    private void OnGridDoubleTapped(object? sender, TappedEventArgs e) {
        DataGrid grid = (sender as DataGrid)!;
        Music item = ((Music)grid.SelectedItem);
        ViewModel?.OnReplacePlaylist(item.Index);
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e) {
        ContentDialog dialog = new() {
            Title = "选择本地音乐文件夹",
            PrimaryButtonText = "确认",
            SecondaryButtonText = "添加文件夹",
            CloseButtonText = "取消",
        };
        ChooseMusicDirViewModel cvm = App.getService<ChooseMusicDirViewModel>();

        dialog.PrimaryButtonClick += (cd, args) => { cvm.save(); };
        dialog.SecondaryButtonClick += async (cd, args) => {
            args.Cancel = true;
            IStorageProvider sp = UiUtils.GetWindow(this).StorageProvider;
            FolderPickerOpenOptions opt = new() {
                AllowMultiple = false,
            };
            IReadOnlyList<IStorageFolder> folders = await sp.OpenFolderPickerAsync(opt);
            foreach (var fol in folders) {
                Uri folPath = fol.Path;
                string path = Uri.UnescapeDataString(folPath.AbsolutePath);
                cvm.addDir(path);
            }

            Console.WriteLine("");
        };
        dialog.Content = new ChooseMusicDirView() {
            DataContext = cvm
        };
        cvm.flushDirs();
        dialog.ShowAsync();
    }

}