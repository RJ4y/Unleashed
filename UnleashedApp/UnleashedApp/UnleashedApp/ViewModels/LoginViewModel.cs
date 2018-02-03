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
        private readonly AuthenticationService _authenticationService;
        private readonly IAuthenticationRepository _authenticationRepository;

        public ICommand PresentLoginScreenCommand { get; set; }

        public LoginViewModel(INavigationService navigationService, AuthenticationService authenticationService, IAuthenticationRepository authenticationRepository)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _authenticationRepository = authenticationRepository;
            GoogleAuthenticator.Authenticator.Completed += OnAuthCompletedAsync;
            GoogleAuthenticator.Authenticator.Error += OnAuthError;

            InitialiseCommands();
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

        private async void OnAuthCompletedAsync(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var googleToken = e.Account.Properties["access_token"];
                CustomTokenResponse tokenResponse = await _authenticationRepository.RequestExchangeGoogleTokenAsync(new TokenConvertRequest(googleToken));
                if (tokenResponse != null)
                {
                    _authenticationService.SaveCredentials(e.Account, tokenResponse);
                    showHomePageAsync();
                }
                else
                {
                    ShowErrorMessage("Oops something went wrong");
                }
            }
            else
            {
                ShowErrorMessage("Oops you are not authorized to use this app");
            }
        }

        private void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            ShowErrorMessage("Oops something went wrong");
        }

        private void ShowErrorMessage(string message)
        {
            MessagingCenter.Send(this, "authentication_failed", message);
        }

        private async void showHomePageAsync()
        {
             await _navigationService.PushAsync(nameof(MenuView));
            //Disables the pressing back (to login) after logging in
            _navigationService.ClearPageStack();
        }

        
    }
}
