using System;
using System.Windows.Input;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts;
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
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthenticationRepository _authenticationRepository;

        public ICommand PresentLoginScreenCommand { get; set; }

        public LoginViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IAuthenticationRepository authenticationRepository)
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
                ((Command)PresentLoginScreenCommand).ChangeCanExecute();
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

        private void showHomePageAsync()
        {
            App.NavigationPage.Navigation.PushAsync(new NameGameView());
            App.NavigationPage.Navigation.RemovePage(App.NavigationPage.Navigation.NavigationStack[0]);
            App.NavigationPage.Navigation.PopToRootAsync();
        }
    }
}
