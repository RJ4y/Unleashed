using System.Collections.Generic;
using UnleashedApp.Models;
using Xamarin.Forms;

namespace UnleashedApp.Services
{
    public static class GridService
    {
        internal static void AddItemToGridAtLocation(View item, Grid grid, int row, int column, bool isInverted = true)
        {
            if (isInverted)
            {
                Grid.SetRow(item, column);
                Grid.SetColumn(item, row);
            }
            else
            {
                Grid.SetRow(item, row);
                Grid.SetColumn(item, column);
            }
            grid.Children.Add(item);
        }

        internal static void AddColorLabel(Grid grid, int row, int column, Color color, bool isInverted = true)
        {
            Label label = new Label { BackgroundColor = color };
            if (isInverted)
            {
                AddItemToGridAtLocation(label, grid, row, column);
            }
            else
            {
                AddItemToGridAtLocation(label, grid, row, column, false);
            }
        }


        internal static void AddTextLabel(Grid grid, int row, int column, string text, bool isInverted = true)
        {
            Label label = new Label { Text = text };
            if (isInverted)
            {
                AddItemToGridAtLocation(label, grid, row, column);
            }
            else
            {
                AddItemToGridAtLocation(label, grid, row, column, false);
            }
        }

        internal static void CreateGridColumnDefinitions(Grid grid, Dimensions dimensions)
        {
            for (int i = 0; i < dimensions.X; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100 / dimensions.X, GridUnitType.Star) });
            }
        }

        internal static void CreateGridRowDefinitions(Grid grid, Dimensions dimensions)
        {
            for (int i = 0; i < dimensions.Y; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100 / dimensions.Y, GridUnitType.Star) });
            }
        }

        internal static Dimensions GetGridTranslation(List<Space> spaces)
        {
            int xMin = int.MaxValue;
            int yMin = int.MaxValue;
            foreach (Space s in spaces)
            {
                int x = s.XCoord;
                if (x < xMin)
                {
                    xMin = x;
                }

                int y = s.YCoord;
                if (y < yMin)
                {
                    yMin = y;
                }
            }
            return new Dimensions(xMin, yMin);
        }

        internal static Dimensions GetMinifiedGridDimensions(List<Space> spaces)
        {
            int xMin = int.MaxValue;
            int yMin = int.MaxValue;
            int xMax = int.MinValue;
            int yMax = int.MinValue;
            foreach (Space s in spaces)
            {
                int x = s.XCoord;
                if (x < xMin)
                {
                    xMin = x;
                }
                if (x > xMax)
                {
                    xMax = x;
                }

                int y = s.YCoord;
                if (y < yMin)
                {
                    yMin = y;
                }
                if (y > yMax)
                {
                    yMax = y;
                }
            }
            int xDifference = xMax - xMin;
            int yDifference = yMax - yMin;

            if (xDifference <= yDifference)
            {
                return new Dimensions(yDifference + 1, yDifference + 1);
            }
            else
            {
                return new Dimensions(xDifference + 1, xDifference + 1);
            }
        }

        internal static Dimensions GetFloorplanGridDimensions(List<Space> spaces, bool isInverted = true)
        {
            int x = 0;
            int y = 0;
            foreach (Space space in spaces)
            {
                if (space.XCoord > x)
                {
                    x = space.XCoord;
                }
                if (space.YCoord > y)
                {
                    y = space.YCoord;
                }
            }
            //Because 0-10 = 11 values are increased by 1;
            if (isInverted)
            {
                return new Dimensions(y + 1, x + 1);
            }
            else
            {
                return new Dimensions(x + 1, y + 1);
            }
        }
    }
}
