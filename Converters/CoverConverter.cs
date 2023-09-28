using System;
using System.Globalization;
using Avalonia.Data.Converters;
using kon.Utils;

namespace kon.Converters;

public class CoverConverter : IValueConverter {

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value == null || value.ToString() == "") {
            return null;
        }

        return CommonUtils.ParseCover((string)value, 48);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }

}