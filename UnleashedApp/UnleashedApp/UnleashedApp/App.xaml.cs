﻿using UnleashedApp.Authentication;
using UnleashedApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace UnleashedApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AuthenticationService authService = new AuthenticationService();
            if(authService.UserIsLoggedIn())
            {
                MainPage = new NavigationPage(new MenuView());
            }
            else
            {
                MainPage = new NavigationPage(new LoginView());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
