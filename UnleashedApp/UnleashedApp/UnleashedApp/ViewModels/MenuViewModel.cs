using System.Net.Http;
using System.Windows.Input;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class MenuViewModel : ViewModelBase, IMenuViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthenticationRepository _authenticationRepository;

        public ICommand WhoIsWhoCommand { get; set; }
        public ICommand FloorplanCommand { get; set; }
        public ICommand NameGameCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

        public MenuViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IAuthenticationRepository authenticationRepository)
        {
            _navigationService = navigationService;
            _authenticationRepository = authenticationRepository;
            _authenticationService = authenticationService;
            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            WhoIsWhoCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(WhoIsWhoView));
            });
            FloorplanCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(FloorplanView));
            });
            NameGameCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(NameGameView));
            });
            LogOutCommand = new Command(async () =>
            {
                //bool revokeSucceeded = await _authenticationRepository.RequestRevokeTokens();
                //if (revokeSucceeded)
                if(true)
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
