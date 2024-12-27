using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_Mvvm_Version.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Om värdet är null eller en tom sträng, döljer vi elementet
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); // Omvänd konvertering används inte
        }
    }
}
