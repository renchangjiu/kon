using Avalonia;
using Avalonia.Controls;
using kon.Views;

namespace kon.Utils;

public static class UiUtils {

    public static MainWindow GetMainWindow(Visual? visual) {
        TopLevel? topLevel = TopLevel.GetTopLevel(visual);
        return (topLevel as MainWindow)!;
    }

}