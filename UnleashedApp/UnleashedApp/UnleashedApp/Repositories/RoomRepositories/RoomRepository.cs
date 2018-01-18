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
                new Room(1,"EMPTY",Color.DarkGray,RoomType.Empty),
                new Room(2,"2FREEDOM",Color.BlueViolet),
             new Room(3,"THE_WORKSHOP",Color.HotPink),
                new Room(4,"FINANCE",Color.DarkOrange),
                new Room(5,"CEO",Color.DarkSeaGreen),
                new Room(6,"PEOPLE",Color.DarkSalmon),
                new Room(7,"KITCHEN",Color.Coral,RoomType.Kitchen),

                new Room(8,"VIKING_DEALS",Color.Brown),
                new Room(9,"JIM_MOBILE",Color.DarkSlateBlue),
             new Room(10,"THE_ARENA",Color.Red),
                new Room(11,"STIEVIE",Color.SteelBlue),
                new Room(12,"TECHNOLOGY",Color.GreenYellow),
                new Room(13,"THE_BIG_ROOM",Color.Indigo),
                new Room(14,"THE_CHAT_ROOM",Color.LightSteelBlue),

                new Room(15,"MOBILE_VIKINGS_PRODUCT",Color.DarkCyan),
             new Room(16,"THE_SPOTLIGHT",Color.Yellow),
                new Room(17,"DESIGN",Color.Maroon),
                new Room(18,"MOVILE_VIKINGS_GET_&_RETAIN",Color.MidnightBlue),
             new Room(19,"THE_CLOUD",Color.Blue),
             new Room(20,"THE_MILKYWAY",Color.Green),
                new Room(21,"THE_WINDOW_ROOM",Color.Navy),
                new Room(22,"COPYROOM",Color.Peru),
                new Room(23,"CUSTOMER_CARE",Color.Purple),
            };
        }
    }
}
