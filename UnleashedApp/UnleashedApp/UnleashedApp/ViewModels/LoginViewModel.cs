using System;
using System.Windows.Input;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.Views;
using Xamarin.Auth;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthenticationRepository _authenticationRepository;

        public ICommand PresentLoginScreenCommand { get; set; }

        bool canLogin = true;

        public LoginViewModel(IAuthenticationService authenticationService, IAuthenticationRepository authenticationRepository)
        {
            _authenticationService = authenticationService;
            _authenticationRepository = authenticationRepository;
            GoogleAuthenticator.Authenticator.Completed += OnAuthCompletedAsync;
            GoogleAuthenticator.Authenticator.Error += OnAuthError;

            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            PresentLoginScreenCommand = new Command(async () => PresentGoogleLoginScreen(), () => canLogin);
        }

        private void PresentGoogleLoginScreen()
        {
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(GoogleAuthenticator.Authenticator);
        }

        private async void OnAuthCompletedAsync(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                ChangeEnableButton(false);
                User user = await _authenticationRepository.GetUserInfoAsync(e.Account);

                //Only @unleashed.be may log in -> disabled for demo purposes

                //if (!user.Email.Contains("@unleashed.be"))
                //{
                    //ChangeEnableButton(true);
                //    ShowErrorMessage("Oops you are not authorized to use this app, make sure you use your @unleashed.be mail address");
                //}
                //else
                //{
                var googleToken = e.Account.Properties["access_token"];
                    CustomTokenResponse tokenResponse = await _authenticationRepository.RequestExchangeGoogleTokenAsync(new TokenConvertRequest(googleToken));
                    if (tokenResponse != null)
                    {
                        _authenticationService.SaveCredentials(e.Account, tokenResponse);
                        _authenticationService.SaveUserName(user);
                        showHomePageAsync();
                    }
                    else
                    {
                        ChangeEnableButton(true);
                        ShowErrorMessage("Oops something went wrong");
                    }
                //}
            }
            else
            {
                ChangeEnableButton(true);
                ShowErrorMessage("Oops you are not authorized to use this app");
            }
        }

        private void ChangeEnableButton(bool isEnabled)
        {
            canLogin = isEnabled;
            ((Command)PresentLoginScreenCommand).ChangeCanExecute();
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
