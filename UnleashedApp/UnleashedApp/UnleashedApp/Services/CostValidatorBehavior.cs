using System;
using UnleashedApp.ViewModels;
using Xamarin.Forms;

namespace UnleashedApp.Services
{
    public class CostValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += HandleTextChanged;
            base.OnAttachedTo(entry);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = ((Entry)sender).BindingContext;

            bool validPrice = decimal.TryParse(e.NewTextValue, out decimal priceDecimal) && priceDecimal >= 0 && priceDecimal <= ((TrainingViewModel) vm).BudgetRemaining && priceDecimal * 100 == Math.Floor(priceDecimal*100);

            ((Entry) sender).TextColor = validPrice ? Color.DarkGray : Color.Red;

            
            ((TrainingViewModel)vm).IsCostValid = validPrice;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
