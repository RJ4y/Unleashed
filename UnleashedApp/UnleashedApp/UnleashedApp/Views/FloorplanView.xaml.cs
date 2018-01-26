using System;
using System.Collections.Generic;
using UnleashedApp.Models;
using UnleashedApp.Services;
using UnleashedApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static UnleashedApp.Models.Room;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloorplanView : ContentPage
    {
        private static FloorplanViewModel viewModel;
        public static List<Room> Rooms { get; private set; }
        public static List<Space> Spaces { get; private set; }
        public static Room SelectedRoom { get; private set; }

        public FloorplanView()
        {
            InitializeComponent();
            viewModel = ViewModelLocator.Instance.FloorplanViewModel;
            Rooms = viewModel.Rooms;
            Spaces = viewModel.Spaces;
            if (Rooms != null && Spaces != null)
            {
                CreateLegendGrid();
                CreateFloorplanGrid();
            }
            else
            {
                Label label = new Label();
                label.Text = "The data could not be found, there may be a problem with your connection.";
                scrollView.Content = label;
                mainGrid.IsVisible = false;
            }
        }

        /// <summary>
        /// Makes the FloorplanGrid cells a square by making the width and height the correct ratio.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            double amountOfColumns = floorplanGrid.ColumnDefinitions.Count;
            double amountOfRows = floorplanGrid.RowDefinitions.Count;
            if (amountOfColumns > 0 && amountOfRows > 0)
            {
                floorplanGrid.HeightRequest = width / amountOfColumns * amountOfRows;
            }
        }

        private void ChangeLegendVisibility(object sender, EventArgs e)
        {
            if (legendGrid.IsVisible)
            {
                legendGrid.IsVisible = false;
                legendButton.Text = "Open legend";
            }
            else
            {
                legendGrid.IsVisible = true;
                legendButton.Text = "Close legend";
            }
        }

        #region LegendGrid
        private void CreateLegendGrid()
        {
            LegendGridService.CreateLegendGridColumnDefinitions(legendGrid);
            LegendGridService.CreateLegendGridRowDefinitions(legendGrid, Rooms.Count);
            FillLegendGrid();
        }

        private void FillLegendGrid()
        {
            int i = 1;
            foreach (Room room in Rooms)
            {
                GridService.AddColorLabel(legendGrid, i, 1, room.Color, false);
                GridService.AddTextLabel(legendGrid, i, 2, room.Name, false);
                i++;
            }
        }
        #endregion

        #region FloorplanGrid
        private void CreateFloorplanGrid()
        {
            Dimensions dimensions = GridService.GetFloorplanGridDimensions(Spaces);
            GridService.CreateGridColumnDefinitions(floorplanGrid, dimensions);
            GridService.CreateGridRowDefinitions(floorplanGrid, dimensions);
            FillFloorplanGrid();
        }

        private void FillFloorplanGrid()
        {
            foreach (Space space in Spaces)
            {
                AddCell(space.XCoord, space.YCoord, Rooms.Find(r => r.Id == space.RoomId));
            }
        }

        private void AddCell(int column, int row, Room room)
        {
            BoxView box = new BoxView { BackgroundColor = room.Color };

            switch (room.Type)
            {
                case RoomType.Kitchen:
                    AddKitchenTapEventToBox(box);
                    break;
                case RoomType.Workspace:
                    AddRoomTapEventToBox(box);
                    break;
            }

            GridService.AddItemToGridAtLocation(box, floorplanGrid, row, column);
        }

        private void AddRoomTapEventToBox(BoxView box)
        {
            AddTapGestureRecognizer(box, RoomClicked);
        }

        private void AddKitchenTapEventToBox(BoxView box)
        {
            AddTapGestureRecognizer(box, KitchenClicked);
        }

        private void AddTapGestureRecognizer(BoxView box, EventHandler handler)
        {
            TapGestureRecognizer box_tap = new TapGestureRecognizer();
            box_tap.Tapped += handler;
            box.GestureRecognizers.Add(box_tap);
        }

        private void RoomClicked(object sender, EventArgs e)
        {
            BoxView box = ((BoxView)sender);
            foreach (Room r in Rooms)
            {
                if (box.BackgroundColor.Equals(r.Color))
                {
                    SelectedRoom = r;
                    TransferService.Store(SelectedRoom, Spaces);
                    break;
                }
            }
            viewModel.RoomCommand.Execute(SelectedRoom);
        }

        private void KitchenClicked(object sender, EventArgs e)
        {
            BoxView box = ((BoxView)sender);
            DisplayAlert("Kitchen", "Kitchen information", "OK");
        }
        #endregion       
    }
}