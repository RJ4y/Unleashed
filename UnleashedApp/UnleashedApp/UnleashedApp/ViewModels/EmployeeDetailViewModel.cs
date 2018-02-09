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

        public EmployeeDetailViewModel(ISpaceRepository spaceRepository, IRoomRepository roomRepository)
        {
            Spaces = spaceRepository.GetAllSpaces();
            Rooms = roomRepository.GetAllRooms();
        }
    }
}