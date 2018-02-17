using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Services;
using UnleashedApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            MessagingCenter.Subscribe<LoginViewModel, string>(this, "authentication_failed",
                (sender, arg) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Authentication failed!", arg, "Try again");
                    });
                });
        }

        protected override void OnAppearing()
        {
            ViewModelLocator.Instance.LoginViewModel.ChangeEnableButton(true);
        }
    }
}