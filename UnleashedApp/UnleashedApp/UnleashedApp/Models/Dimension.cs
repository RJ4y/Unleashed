namespace UnleashedApp.Models
{
    public class Dimension
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Dimension(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
