namespace UnleashedApp.Models
{
    public class SerializableRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorAsString { get; set; }
        public string TypeAsString { get; set; }

        public SerializableRoom(int id, string name, string color, string type)
        {
            Id = id;
            Name = name;
            ColorAsString = color;
            TypeAsString = type;
        }
    }
}
