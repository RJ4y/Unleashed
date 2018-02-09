using System.Collections.Generic;
using System.Windows.Input;
using UnleashedApp.Contracts;
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
        private readonly ISpaceRepository _spaceRepository;
        private readonly IRoomRepository _roomRepository;

        public ICommand RoomCommand { get; set; }

        public List<Space> Spaces { get; set; }
        public List<Room> Rooms { get; set; }


        public FloorplanViewModel(INavigationService navigationService, ISpaceRepository spaceRepository,
            IRoomRepository roomRepository)
        {
            _navigationService = navigationService;
            _spaceRepository = spaceRepository;
            _roomRepository = roomRepository;

            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            RoomCommand = new Command(() =>
            {
                App.NavigationPage.Navigation.PushAsync(new RoomView());
            });
        }

        public void LoadSpaces()
        {
            if (Spaces == null)
            {
                Spaces = _spaceRepository.GetAllSpaces();
            }
        }

        public void LoadRooms()
        {
            if (Rooms == null)
            {
                Rooms = _roomRepository.GetAllRooms();
            }

        }
    }
}