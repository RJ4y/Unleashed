using System.Windows.Input;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class MenuViewModel : ViewModelBase, IMenuViewModel
    {
        private readonly INavigationService _navigationService;

        public ICommand HomeCommand { get; set; }
        public ICommand WhoIsWhoCommand { get; set; }
        public ICommand FloorplanCommand { get; set; }

        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            HomeCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(MenuView));
            });
            WhoIsWhoCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(WhoIsWhoView));
            });
            FloorplanCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(FloorplanView));
            });
        }
    }
}
