using System.Windows.Input;
using UnleashedApp.Contracts;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class MenuViewModel : ViewModelBase, IMenuViewModel
    {
        private readonly INavigationService _navigationService;

        public ICommand WhoIsWhoCommand { get; set; }
        public ICommand FloorplanCommand { get; set; }
        public ICommand NameGameCommand { get; set; }
        public ICommand TrainingCommand { get; set; }

        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
            TrainingCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(TrainingView));
            });
        }
    }
}