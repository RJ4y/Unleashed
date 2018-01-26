using System.Collections.Generic;
using System.Windows.Input;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using UnleashedApp.Repositories.RoomRepositories;
using UnleashedApp.Repositories.SpaceRepositories;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class FloorplanViewModel : ViewModelBase, IFloorplanViewModel
    {
        private readonly INavigationService _navigationService;

        public ICommand RoomCommand { get; set; }

        private ISpaceRepository _spaceRepository;
        private IRoomRepository _roomRepository;

        public List<Space> Spaces { get; set; }
        public List<Room> Rooms { get; set; }


        public FloorplanViewModel(INavigationService navigationService, ISpaceRepository spaceRepository, IRoomRepository roomRepository)
        {
            _navigationService = navigationService;

            _spaceRepository = spaceRepository;
            _roomRepository = roomRepository;

            InitialiseCommands();

            Spaces = _spaceRepository.GetAllSpaces();
            Rooms = _roomRepository.GetAllRooms();
        }

        private void InitialiseCommands()
        {            
            RoomCommand = new Command(async () =>
            {
                await _navigationService.PushAsync(nameof(RoomView));
            });
        }
    }
}
