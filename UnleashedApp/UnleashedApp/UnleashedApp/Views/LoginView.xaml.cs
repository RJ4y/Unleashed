using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;
using UnleashedApp.Repositories;
using UnleashedApp.Repositories.AuthenticationRepositories;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        AuthenticationRepository repository;

        public LoginView()
        {
            InitializeComponent();
          
            NavigationPage.SetHasNavigationBar(this, false);

            repository = new AuthenticationRepository();

            GoogleAuthenticator.Authenticator.Completed += OnAuthCompleted;

            loginButton.Clicked += PresentGoogleLoginScreen;
        }

        private void PresentGoogleLoginScreen(object sender, EventArgs e)
        {
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(GoogleAuthenticator.Authenticator);
        }

        private void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            DisplayAlert("auth completed", "auth completed", "xx");
            if (e.IsAuthenticated)
            {
                var googleToken = e.Account.Properties["access_token"];
                DisplayAlert("is authenticated", "is authenticated", "xx");

                //Get custom access token API
                CustomTokenResponse accessToken = repository.GetCustomToken(new TokenConvertRequest(googleToken)).Result;
                repository.SaveCredentials(e.Account, accessToken.access_token, googleToken);
            }
            else
            {
                // The user is not authenticated
            }
        }

        

    }
}