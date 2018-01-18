using System;
using System.Windows.Input;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.Views;
using Xamarin.Auth;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        private readonly INavigationService _navigationService;
        private AuthenticationRepository repository;

        public ICommand PresentLoginScreenCommand { get; set; }

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            repository = new AuthenticationRepository();
            GoogleAuthenticator.Authenticator.Completed += OnAuthCompletedAsync;
            //GoogleAuthenticator.Authenticator.Error += OnAuthErrorAsync;

            InitialiseCommands();
        }

        private async void OnAuthCompletedAsync(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var googleToken = e.Account.Properties["access_token"];

                //Get custom access token API
                CustomTokenResponse accessToken = await repository.GetCustomTokenAsync(new TokenConvertRequest(googleToken));
                if (accessToken != null)
                {
                    repository.SaveCredentials(e.Account, accessToken.access_token, googleToken);

                    await _navigationService.PushAsync(nameof(MenuView));
                    //Disables the pressing back (to login) after logging in
                    _navigationService.ClearPageStack();
                }
                else
                {
                    MessagingCenter.Send(this, "Authentication_fail", "Oops something went wrong");
                }
            }
            else
            {
                MessagingCenter.Send(this, "Authentication_fail", "Oops something went wrong");
            }
        }

        private void InitialiseCommands()
        {
            PresentLoginScreenCommand = new Command(() =>
                {
                    var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                    presenter.Login(GoogleAuthenticator.Authenticator);
                }
            );
        }
    }
}
