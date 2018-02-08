using System;
using System.Globalization;
using Xamarin.Forms;

namespace UnleashedApp.Converters
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "€" + value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string) value).Replace("€", "");
        }
    }
}
