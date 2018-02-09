using Xamarin.Forms;

namespace UnleashedApp.Services
{
    public class LegendGridService
    {
        public static void CreateLegendGridRowDefinitions(Grid grid, int amountOfLabels)
        {
            grid.RowDefinitions.Add(new RowDefinition {Height = new GridLength(4, GridUnitType.Star)});

            for (int i = 0; i < amountOfLabels; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
            }

            grid.RowDefinitions.Add(new RowDefinition {Height = new GridLength(4, GridUnitType.Star)});
        }

        public static void CreateLegendGridColumnDefinitions(Grid grid)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(2, GridUnitType.Star)});
            grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(10, GridUnitType.Star)});
            grid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(88, GridUnitType.Star)});
        }
    }
}