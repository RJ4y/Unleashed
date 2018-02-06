using System;
using System.Text.RegularExpressions;
using UnleashedApp.ViewModels;
using Xamarin.Forms;

namespace UnleashedApp.Services
{
    public class CityValidatorBehavior : Behavior<Entry>
    {
        const string cityRegex = "^([a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$";

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += HandleTextChanged;
            base.OnAttachedTo(entry);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool validCity = (Regex.IsMatch(e.NewTextValue, cityRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = validCity ? Color.Green : Color.Red;

            var vm = ((Entry)sender).BindingContext;
            ((TrainingViewModel)vm).IsCityValid = validCity;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
