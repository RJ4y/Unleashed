﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Models;

namespace UnleashedApp.Services
{
    public static class TransferService
    {
        private static Room _selectedRoom;
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

        public static Room GetSelectedRoom()
        {
            return _selectedRoom;
        }

        public static List<Space> GetSelectedSpaces()
        {
            return _spaces;
        }
    }
}