using Xamarin.Forms;

namespace UnleashedApp.Models
{
    public class DrawableRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        public RoomType Type { get; set; }

        public DrawableRoom(int id, string name, Color color, RoomType type)
        {
            Id = id;
            Name = name;
            Color = color;
            Type = type;
        }

        public enum RoomType { Empty, Workspace, Kitchen };

    }
}
