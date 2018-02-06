using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnleashedApp.ViewModels;
using Xamarin.Forms;

namespace UnleashedApp.Services
{
    public class CompanyValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += HandleTextChanged;
            base.OnAttachedTo(entry);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool validCompany = e.NewTextValue.Length > 0;
            ((Entry)sender).TextColor = validCompany ? Color.Green : Color.Red;

            var vm = ((Entry)sender).BindingContext;
            ((TrainingViewModel)vm).IsCompanyValid = validCompany;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
