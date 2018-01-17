using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        OAuth2Authenticator authenticator;
        public LoginView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            authenticator = new OAuth2Authenticator(
                "213697302597-efe8ekru9nue7ihl2qocfbe0j8dgmqcp.apps.googleusercontent.com",
                null,
                "email",
                new Uri("https://accounts.google.com/o/oauth2/v2/auth"),
                new Uri("be.unleashed.app:/oauth2redirect"),
                new Uri("https://www.googleapis.com/oauth2/v4/token"),
                null,
                true);
            authenticator.Completed += OnAuthCompleted;

            loginButton.Clicked += PresentGoogleLoginScreen;
        }

        private void PresentGoogleLoginScreen(object sender, EventArgs e)
        {
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        private void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            DisplayAlert("auth completed", "auth completed", "xx");
            if (e.IsAuthenticated)
            {
                // The user is authenticated
                // Extract the OAuth token
                var token_type = e.Account.Properties["token_type"];
                var accessToken = e.Account.Properties["access_token"];
                DisplayAlert("is authenticated", "is authenticated", "xx");
                // Do something
            }
            else
            {
                // The user is not authenticated
            }
        }

    }
}