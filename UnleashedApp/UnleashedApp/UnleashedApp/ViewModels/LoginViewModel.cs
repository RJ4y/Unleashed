using System.Windows.Input;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        private readonly INavigationService _navigationService;
        public ICommand LoginCommand { get; set; }

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            LoginCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(MenuView));
                //Disables the pressing back (to login) after logging in
                _navigationService.ClearPageStack();
            });
        }
    }
}
