using System.Collections.Generic;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using UnleashedApp.Repositories.RoomRepositories;
using UnleashedApp.Repositories.SpaceRepositories;

namespace UnleashedApp.ViewModels
{
    public class EmployeeDetailViewModel : ViewModelBase, IEmployeeDetailViewModel
    {
        public List<Space> Spaces { get; set; }
        public List<Room> Rooms { get; set; }
        private readonly ISpaceRepository _spaceRepository;
        private readonly IRoomRepository _roomRepository;

        public EmployeeDetailViewModel(ISpaceRepository spaceRepository, IRoomRepository roomRepository)
        {
            _spaceRepository = spaceRepository;
            _roomRepository = roomRepository;
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