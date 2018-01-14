using System.Windows.Input;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class FloorplanViewModel : ViewModelBase, IFloorplanViewModel
    {
        private readonly INavigationService _navigationService;
        public ICommand FloorplanCommand { get; set; }
        public ICommand RoomCommand { get; set; }


        public FloorplanViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            FloorplanCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(FloorplanView));
            });
            RoomCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(RoomView));
            });
        }
    }
}
