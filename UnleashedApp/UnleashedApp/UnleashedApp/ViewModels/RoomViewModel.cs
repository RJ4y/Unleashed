using System.Collections.Generic;
using System.Windows.Input;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using UnleashedApp.Services;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class RoomViewModel : ViewModelBase, IRoomViewModel
    {
        private readonly INavigationService _navigationService;

        public ICommand RoomCommand { get; set; }
        public ICommand WhoIsWhoCommand { get; set; }

        public RoomViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitialiseCommands();
        }
        
        private void InitialiseCommands()
        {
            RoomCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(RoomView));
            });
            WhoIsWhoCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(WhoIsWhoView));
            });
        }
    }
}
