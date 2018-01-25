using System.Collections.Generic;
using UnleashedApp.Models;
using Xamarin.Forms;
using static UnleashedApp.Models.Room;

namespace UnleashedApp.Repositories.RoomRepositories
{
    public class RoomRepository : Repository, IRoomRepository
    {
        public List<Room> GetAllRooms()
        {
            return new List<Room> {
                new Room(1, "Empty", Color.DarkGray,RoomType.Empty),
                new Room(2, "2Freedom", Color.BlueViolet),
                new Room(3, "The Workshop", Color.HotPink),
                new Room(4, "Finance", Color.DarkOrange),
                new Room(5, "CEO", Color.DarkSeaGreen),
                new Room(6, "People", Color.DarkSalmon),
                new Room(7, "Kitchen", Color.Coral,RoomType.Kitchen),

                new Room(8, "Viking Deals", Color.Brown),
                new Room(9, "JIM Mobile", Color.DarkSlateBlue),
                new Room(10, "The Arena", Color.Red),
                new Room(11, "Stievie", Color.SteelBlue),
                new Room(12, "Technology", Color.GreenYellow),
                new Room(13, "The Big Room", Color.Indigo),
                new Room(14, "The Chat Room", Color.LightSteelBlue),

                new Room(15, "Mobile Vikings Product", Color.DarkCyan),
                new Room(16, "The Spotlight", Color.Yellow),
                new Room(17, "Design", Color.Maroon),
                new Room(18, "Mobile Vikings Get & Retain", Color.MidnightBlue),
                new Room(19, "The Cloud", Color.Blue),
                new Room(20, "The Milky Way", Color.Green),
                new Room(21, "The Window Room", Color.Navy),
                new Room(22, "Copyroom", Color.Peru),
                new Room(23, "Customer Care", Color.Purple),
            };
        }
    }
}
