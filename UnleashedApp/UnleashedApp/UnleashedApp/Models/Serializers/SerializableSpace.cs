using Newtonsoft.Json;

namespace UnleashedApp.Models
{
    public class SerializableSpace
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        [JsonProperty(PropertyName = "employee_id")]
        public int EmployeeId { get; set; }
        public int Room { get; set; }

        public SerializableSpace(int x, int y, int emp_id, int room_id)
        {
            XCoord = x;
            YCoord = y;
            EmployeeId = emp_id;
            Room = room_id;
        }
    }
}
