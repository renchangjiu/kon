using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace kon.Converters;

public class MusicOrderConverter : IValueConverter {

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        string order = (int)value! + 1 + "";
        return order.PadLeft(2, '0');
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }

}