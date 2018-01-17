using System;
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
        private static Random random = new Random();

        public static void CreateRooms()
        {
            List<Color> randomColors = GetListOfRandomColors(18);

            Rooms = new List<DrawableRoom> {
                new DrawableRoom(1,"EMPTY",Color.LightGray,RoomType.Empty),
                new DrawableRoom(2,"2FREEDOM",randomColors[1]),
                new DrawableRoom(3,"THE_WORKSHOP",Color.HotPink),
                new DrawableRoom(4,"FINANCE",randomColors[2]),
                new DrawableRoom(5,"CEO",randomColors[3]),
                new DrawableRoom(6,"PEOPLE",randomColors[4]),
                new DrawableRoom(7,"KITCHEN",randomColors[5],RoomType.Kitchen),

                new DrawableRoom(8,"VIKING_DEALS",randomColors[6]),
                new DrawableRoom(9,"JIM_MOBILE",randomColors[7]),
                new DrawableRoom(10,"THE_ARENA",Color.Red),
                new DrawableRoom(11,"STIEVIE",randomColors[8]),
                new DrawableRoom(12,"TECHNOLOGY",randomColors[9]),
                new DrawableRoom(13,"THE_BIG_ROOM",randomColors[10]),
                new DrawableRoom(14,"THE_CHAT_ROOM",Color.Red),

                new DrawableRoom(15,"MOBILE_VIKINGS_PRODUCT",randomColors[11]),
                new DrawableRoom(16,"THE_SPOTLIGHT",Color.Yellow),
                new DrawableRoom(17,"DESIGN",randomColors[12]),
                new DrawableRoom(18,"MOVILE_VIKINGS_GET_&_RETAIN",randomColors[13]),
                new DrawableRoom(19,"THE_CLOUD",Color.Blue),
                new DrawableRoom(20,"THE_MILKYWAY",randomColors[14]),
                new DrawableRoom(21,"THE_WINDOW_ROOM",randomColors[15]),
                new DrawableRoom(22,"COPYROOM",randomColors[16]),
                new DrawableRoom(23,"CUSTOMER_CARE",randomColors[17]),
            };
        }

        private static List<Color> GetListOfRandomColors(int amount)
        {
            List<Color> colors = new List<Color>();
            for (int i = 0; i < amount; i++)
            {
                colors.Add(Color.FromRgb(random.Next(256), random.Next(256), random.Next(256)));
            }
            return colors;
        }

        public static void AddColorLabel(Grid grid, int row, int column, Color color)
        {
            Label label = new Label { BackgroundColor = color };
            AddItemToGridAtLocation(label, grid, row, column);
        }

        public static void AddItemToGridAtLocation(View item, Grid grid, int row, int column)
        {
            Grid.SetRow(item, column);
            Grid.SetColumn(item, row);
            grid.Children.Add(item);
        }

        public static Dimension GetRoomGridDimensions()
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
                return new Dimension(yDifference + 1, yDifference + 1);
            }
            else
            {
                return new Dimension(xDifference + 1, xDifference + 1);
            }
        }

        public static DrawableRoom GetSelectedRoom()
        {
            DrawableRoom room = null;
            foreach (Space s in SelectedSpaces)
            {
                if (s.EmployeeId == 0)
                {
                    room = Rooms.Find(r => r.Id == s.RoomId);
                    break;
                }
            }
            return room;
        }

        public static Dimension GetRoomGridTranslation()
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
            return new Dimension(xMin, yMin);
        }

        public static void AddTextLabel(Grid grid, int row, int column, string text)
        {
            Label label = new Label { Text = text };
            AddItemToGridAtLocation(label, grid, row, column);
        }


        public static void CreateGridColumnDefinitions(Grid grid, Dimension dimension)
        {
            for (int i = 0; i < dimension.X; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100 / dimension.X, GridUnitType.Star) });
            }
        }

        public static void CreateGridRowDefinitions(Grid grid, Dimension dimension)
        {
            for (int i = 0; i < dimension.Y; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100 / dimension.Y, GridUnitType.Star) });
            }
        }

        public static void CreateLegendGridRowDefinitions(Grid grid, int amountOfRooms)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4, GridUnitType.Star) });

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
