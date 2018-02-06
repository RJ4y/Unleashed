using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UnleashedApp.Services
{
    public class DaysValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += HandleTextChanged;
            base.OnAttachedTo(entry);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool validDays = int.TryParse(e.NewTextValue, out int days) && days > 0 && days <= 100;

            ((Entry)sender).TextColor = validDays ? Color.Green : Color.Red;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
