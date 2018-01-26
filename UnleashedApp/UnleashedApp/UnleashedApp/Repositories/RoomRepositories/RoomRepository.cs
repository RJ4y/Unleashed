using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using UnleashedApp.Models;
using Xamarin.Forms;
using static UnleashedApp.Models.Room;

namespace UnleashedApp.Repositories.RoomRepositories
{
    public class RoomRepository : Repository, IRoomRepository
    {
        private List<Room> _rooms = new List<Room>();

        public List<Room> GetAllRooms()
        {
            string address = "rooms/";
            try
            {
                HttpResponseMessage response = _client.GetAsync(address).Result;

                if (response.IsSuccessStatusCode)
                {
                    string resultString = response.Content.ReadAsStringAsync().Result;
                    List<SerializableRoom> rooms = JsonConvert.DeserializeObject<List<SerializableRoom>>(resultString);
                    foreach (SerializableRoom r in rooms)
                    {
                        Color color = Color.FromHex("#" + r.ColorAsString);

                        RoomType type;
                        switch (r.TypeAsString)
                        {
                            case "Empty":
                                type = RoomType.Empty;
                                break;
                            case "Workspace":
                                type = RoomType.Workspace;
                                break;
                            case "Kitchen":
                                type = RoomType.Kitchen;
                                break;
                            default:
                                type = RoomType.Invalid;
                                break;
                        }

                        Room room = new Room(r.Id, r.Name, color, type);
                        _rooms.Add(room);
                    }
                }
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return _rooms;
        }
    }
}
