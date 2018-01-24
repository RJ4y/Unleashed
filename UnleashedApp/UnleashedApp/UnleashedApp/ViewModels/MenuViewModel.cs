using System.Windows.Input;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class MenuViewModel : ViewModelBase, IMenuViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly AuthenticationService _authenticationService;

        public ICommand WhoIsWhoCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

        public MenuViewModel(INavigationService navigationService, AuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            WhoIsWhoCommand = new Command(async() =>
            {
                await _navigationService.PushAsync(nameof(WhoIsWhoView));
            });

            LogOutCommand = new Command(async() =>
            {
                _authenticationService.DeleteAccessToken();
                await _navigationService.PushAsync(nameof(LoginView));
            });
        }
    }
}
