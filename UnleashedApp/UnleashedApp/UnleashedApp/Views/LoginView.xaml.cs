using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;
using UnleashedApp.Repositories;
using UnleashedApp.ViewModels;
using Xamarin.Auth;
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
            MessagingCenter.Subscribe<LoginViewModel>(this, "Authentication_fail", (sender) =>
            {
                DisplayAlert("Authentication failed!", "Oops something went wrong", "Try again");
            });
        }

    }
}