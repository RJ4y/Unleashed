using System;
using UnleashedApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhoIsWhoView: ContentPage
    {
        public WhoIsWhoView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.WhoIsWhoViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext != null)
            {
                ((WhoIsWhoViewModel)BindingContext).LoadEmployeesPerHabitat();
                ((WhoIsWhoViewModel)BindingContext).RefreshFilteredList();
                ((WhoIsWhoViewModel)BindingContext).LoadEmployeesPerSquad();
                ((WhoIsWhoViewModel)BindingContext).RefreshFilter();
            }
        }

        private void HabitatButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            button.BackgroundColor = Color.DodgerBlue;
            button.TextColor = Color.White;
            SquadButton.BackgroundColor = Color.White;
            SquadButton.TextColor = Color.DodgerBlue;
        }

        private void SquadButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundColor = Color.DodgerBlue;
            button.TextColor = Color.White;
            HabitatButton.BackgroundColor = Color.White;
            HabitatButton.TextColor = Color.DodgerBlue;
        }
    }
}