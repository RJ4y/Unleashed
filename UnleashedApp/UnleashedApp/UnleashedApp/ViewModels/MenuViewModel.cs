using System.Windows.Input;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class MenuViewModel : ViewModelBase, IMenuViewModel
    {
        private readonly INavigationService _navigationService;

        public ICommand WhoIsWhoCommand { get; set; }

        public MenuViewModel(INavigationService navigationService)
        {
            //InitialiseComponents(navigationService);
            _navigationService = navigationService;
            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            WhoIsWhoCommand = new Command(async() =>
            {
                await NavigationService.PushAsync(nameof(WhoIsWhoView));
            });
        }
    }
}
