using Avalonia;
using Avalonia.Controls;
using kon.Views;

namespace kon.Utils;

public static class UiUtils {

    public static Window GetWindow(Visual? visual) {
        TopLevel? topLevel = TopLevel.GetTopLevel(visual);
        return (topLevel as Window)!;
    }

}