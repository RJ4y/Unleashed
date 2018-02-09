using System;
using System.Globalization;
using Xamarin.Forms;

namespace UnleashedApp.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse(value.ToString(), out DateTime date))
            {
                return $"{date:dd/MM/yyyy}";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}