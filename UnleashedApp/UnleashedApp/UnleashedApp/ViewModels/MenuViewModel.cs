using System.Windows.Input;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class MenuViewModel : ViewModelBase, IMenuViewModel
    {
        private readonly INavigationService _navigationService;

        public ICommand WhoIsWhoCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //InitialiseComponents(messagingCenter, navigationService);
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
                AuthenticationRepository authRepo = new AuthenticationRepository();
                authRepo.DeleteAccessToken();
                await _navigationService.PushAsync(nameof(LoginView));
            });
        }
    }
}
