using System.Collections.Generic;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.RoomRepositories
{
    public interface IRoomRepository
    {
        List<Room> GetAllRooms();
    }
}