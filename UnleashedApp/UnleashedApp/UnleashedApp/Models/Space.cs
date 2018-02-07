using Newtonsoft.Json;

namespace UnleashedApp.Models
{
    public class Space
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }

        [JsonProperty(PropertyName = "employee")]
        public int? EmployeeId { get; set; }

        [JsonProperty(PropertyName = "room")] public int RoomId { get; set; }

        public Space(int x, int y, int? empId, int roomId)
        {
            XCoord = x;
            YCoord = y;
            EmployeeId = empId;
            RoomId = roomId;
        }
    }
}