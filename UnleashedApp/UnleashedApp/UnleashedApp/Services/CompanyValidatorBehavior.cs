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
            ((Entry)sender).TextColor = validCompany ? Color.DarkGray : Color.Red;

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
