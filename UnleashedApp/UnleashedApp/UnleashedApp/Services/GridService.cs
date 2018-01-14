using System.Collections.Generic;
using UnleashedApp.Models;
using Xamarin.Forms;
using static UnleashedApp.Models.DrawableRoom;

namespace UnleashedApp.Services
{
    public static class GridService
    {
        public static List<Space> SelectedSpaces { get; private set; }
        public static List<DrawableRoom> Rooms { get; private set; }

        public static void CreateRooms()
        {
            Rooms = new List<DrawableRoom> {
                new DrawableRoom(1,"Hallway",Color.Blue,RoomType.Empty),
                new DrawableRoom(2,"Kitchen",Color.Green,RoomType.Kitchen),
                new DrawableRoom(3,"Workspace",Color.Black,RoomType.Workspace)
            };
        }

        public static void AddColorLabel(Grid grid, int row, int column, Color color)
        {
            Label label = new Label { BackgroundColor = color };
            AddItemToGridAtLocation(label, grid, row, column);
        }

        public static void AddItemToGridAtLocation(View item, Grid grid, int row, int column)
        {
            Grid.SetRow(item, row);
            Grid.SetColumn(item, column);
            grid.Children.Add(item);
        }

        public static Size GetRoomGridDimensions()
        {
            int xMin = int.MaxValue;
            int yMin = int.MaxValue;
            int xMax = int.MinValue;
            int yMax = int.MinValue;
            foreach (Space s in SelectedSpaces)
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
                return new Size(yDifference + 1, yDifference + 1);
            }
            else
            {
                return new Size(xDifference + 1, xDifference + 1);
            }
        }

        public static Size GetRoomGridTranslation()
        {
            int xMin = int.MaxValue;
            int yMin = int.MaxValue;
            foreach (Space s in SelectedSpaces)
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
            return new Size(xMin, yMin);
        }

        public static void AddTextLabel(Grid grid, int row, int column, string text)
        {
            Label label = new Label { Text = text };
            AddItemToGridAtLocation(label, grid, row, column);
        }


        public static void CreateGridColumnDefinitions(Grid grid, Size size)
        {
            for (int i = 0; i < size.Width; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100 / size.Width, GridUnitType.Star) });
            }
        }

        public static void CreateGridRowDefinitions(Grid grid, Size size)
        {
            for (int i = 0; i < size.Height; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100 / size.Height, GridUnitType.Star) });
            }
        }

        public static void CreateLegendGridRowDefinitions(Grid grid, int amountOfRooms)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(6, GridUnitType.Star) });

            for (int i = 0; i < amountOfRooms; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }
        }

        public static void CreateLegendGridColumnDefinitions(Grid grid)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(88, GridUnitType.Star) });
        }

        public static void StoreSpacesFromSelectedRoom(int id, List<Space> spaces)
        {
            SelectedSpaces = new List<Space>();
            foreach (Space s in spaces)
            {
                if (s.RoomId == id)
                {
                    SelectedSpaces.Add(s);
                }
            }
        }
    }
}
