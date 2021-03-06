﻿using System.Collections.Generic;
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

        public static void AddColorLabel(Grid grid, int row, int column, Color color, bool isInverted = true)
        {
            Label label = new Label {BackgroundColor = color};
            if (isInverted)
            {
                AddItemToGridAtLocation(label, grid, row, column);
            }
            else
            {
                AddItemToGridAtLocation(label, grid, row, column, false);
            }
        }


        public static void AddTextLabel(Grid grid, int row, int column, string text, bool isInverted = true)
        {
            Label label = new Label {Text = text};
            if (isInverted)
            {
                AddItemToGridAtLocation(label, grid, row, column);
            }
            else
            {
                AddItemToGridAtLocation(label, grid, row, column, false);
            }
        }

        public static void CreateGridColumnDefinitions(Grid grid, Dimensions dimensions)
        {
            for (int i = 0; i < dimensions.X; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(100 / dimensions.X, GridUnitType.Star)
                });
            }
        }

        public static void CreateGridRowDefinitions(Grid grid, Dimensions dimensions)
        {
            for (int i = 0; i < dimensions.Y; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(100 / dimensions.Y, GridUnitType.Star)
                });
            }
        }

        public static Dimensions GetGridTranslation(List<Space> spaces)
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

        public static Dimensions GetMinifiedSquareGridDimensions(List<Space> spaces)
        {
            Dimensions dimensions = GetDifferenceAsDimension(spaces);
            int xDifference = dimensions.X;
            int yDifference = dimensions.Y;

            if (xDifference <= yDifference)
            {
                return new Dimensions(yDifference + 1, yDifference + 1);
            }

            return new Dimensions(xDifference + 1, xDifference + 1);
        }

        public static Dimensions GetDifferenceAsDimension(List<Space> spaces, bool isInverted = true)
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

            if (isInverted)
            {
                return new Dimensions(yDifference + 1, xDifference + 1);
            }

            return new Dimensions(xDifference + 1, yDifference + 1);
        }
    }
}