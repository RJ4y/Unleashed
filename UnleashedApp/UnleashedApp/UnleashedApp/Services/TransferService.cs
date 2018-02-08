using System.Collections.Generic;
using UnleashedApp.Models;

namespace UnleashedApp.Services
{
    public static class TransferService
    {
        private static Room _selectedRoom;
        private static Employee _selectedEmployee;
        private static List<Space> _spaces;

        internal static void Store(Room selectedRoom, List<Space> spaces)
        {
            _selectedRoom = selectedRoom;

            _spaces = new List<Space>();
            foreach (Space s in spaces)
            {
                if (s.RoomId == selectedRoom.Id)
                {
                    _spaces.Add(s);
                }
            }
        }

        internal static Room GetSelectedRoom()
        {
            return _selectedRoom;
        }

        internal static List<Space> GetSelectedSpaces()
        {
            return _spaces;
        }

        internal static void Store(Employee employee)
        {
            _selectedEmployee = employee;
        }

        internal static Employee GetSelectedEmployee()
        {
            return _selectedEmployee;
        }
    }
}
