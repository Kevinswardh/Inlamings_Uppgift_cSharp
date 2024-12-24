using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF_Version.Converters
{
    public class FavoriteToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Kontrollera om värdet är true (favorit) och returnera rätt färg
            if (value is bool isFavorite && isFavorite)
            {
                return Brushes.Gold; // Favorit visas med guld
            }
            return Brushes.Gray; // Icke-favorit visas med grå färg
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); // Omvänd konvertering används inte
        }
    }
}
