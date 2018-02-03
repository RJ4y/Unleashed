using System.Windows.Input;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class MenuViewModel : ViewModelBase, IMenuViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly AuthenticationService _authenticationService;
        private readonly IAuthenticationRepository _authenticationRepository;

        public ICommand WhoIsWhoCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

        public MenuViewModel(INavigationService navigationService, AuthenticationService authenticationService, IAuthenticationRepository authenticationRepository)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _authenticationRepository = authenticationRepository;
            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            WhoIsWhoCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(WhoIsWhoView));
            });

            LogOutCommand = new Command(async () =>
            {
                bool revokeSucceeded = await _authenticationRepository.RequestRevokeTokens();
                if (revokeSucceeded)
                {
                _authenticationService.DeleteAccessTokens();
                    await _navigationService.PushAsync(nameof(LoginView));
                }
                else
                {
                    MessagingCenter.Send(this, "logout_failed");
                }
            });
        }
    }
}
