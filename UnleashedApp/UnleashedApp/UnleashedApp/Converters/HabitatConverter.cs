using System;
using System.Globalization;
using UnleashedApp.Models;
using UnleashedApp.Repositories.HabitatRepositories;
using Xamarin.Forms;

namespace UnleashedApp.Converters
{
    public class HabitatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Habitat habitat = new HabitatRepository().GetHabitatById((int) value);
            return habitat.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Habitat habitat = (Habitat) value;
            return habitat.Id;
        }
    }
}