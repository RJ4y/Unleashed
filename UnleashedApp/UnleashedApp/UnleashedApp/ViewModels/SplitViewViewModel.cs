using System.Windows.Input;
using UnleashedApp.Contracts;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class SplitViewViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthenticationRepository _authenticationRepository;

        public ICommand GoHomeCommand { get; set; }
        public ICommand GoWhoIsWhoCommand { get; set; }
        public ICommand GoFloorplanCommand { get; set; }
        public ICommand GoNameGameCommand { get; set; }
        public ICommand GoLogoutCommand { get; set; }

        public SplitViewViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IAuthenticationRepository authenticationRepository)
        {
            _navigationService = navigationService;
            _authenticationRepository = authenticationRepository;
            _authenticationService = authenticationService;
            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            GoHomeCommand = new Command(() =>
            {
                App.NavigationPage.Navigation.PopToRootAsync();
                App.MenuIsPresented = false;
            });
            GoWhoIsWhoCommand = new Command(() =>
            {
                App.NavigationPage.Navigation.PushAsync(new WhoIsWhoView());
                App.MenuIsPresented = false;
            });
            GoFloorplanCommand = new Command(() =>
            {
                App.NavigationPage.Navigation.PushAsync(new FloorplanView());
                App.MenuIsPresented = false;
            });
            GoNameGameCommand = new Command(() =>
            {
                App.NavigationPage.Navigation.PushAsync(new NameGameView());
                App.MenuIsPresented = false;
            });
            GoLogoutCommand = new Command(() =>
            {
                _authenticationService.DeleteAccessTokens();
                App.NavigationPage.Navigation.PushAsync(new LoginView());
                App.MenuIsPresented = false;
            });
        }
    }
}
