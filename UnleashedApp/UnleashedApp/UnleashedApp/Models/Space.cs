namespace UnleashedApp.Models
{
    public class Space
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public int EmployeeId { get; set; }
        public int RoomId { get; set; }

        public Space(int x, int y, int emp_id, int room_id)
        {
            XCoord = x;
            YCoord = y;
            EmployeeId = emp_id;
            RoomId = room_id;
        }
    }
}
