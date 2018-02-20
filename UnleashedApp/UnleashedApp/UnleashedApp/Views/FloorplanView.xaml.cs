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
    public partial class FloorplanView: TabbedPage
    {
        private static FloorplanViewModel _viewModel;
        public static List<Room> Rooms { get; private set; }
        public static List<Space> Spaces { get; private set; }
        public static Room SelectedRoom { get; private set; }
        private bool shouldLoad = true;

        public FloorplanView()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModelLocator.Instance.FloorplanViewModel != null)
            {
                ViewModelLocator.Instance.FloorplanViewModel.LoadSpaces();
                ViewModelLocator.Instance.FloorplanViewModel.LoadRooms();
               

                if (shouldLoad)
                {
                    _viewModel = ViewModelLocator.Instance.FloorplanViewModel;
                    Rooms = _viewModel.Rooms;
                    Spaces = _viewModel.Spaces;
                    if (Rooms != null && Rooms.Count > 0 && Spaces != null && Spaces.Count > 0)
                    {
                        CreateLegendGrid();
                        CreateFloorplanGrid();
                    }
                    else
                    {
                        Label label = new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "The data could not be found, there may be a problem with your connection."
                        };
                        FloorplanScrollView.Content = label;
                        LegendScrollView.Content = label;
                        FloorplanGrid.IsVisible = false;
                        LegendGrid.IsVisible = false;
                    }

                    shouldLoad = false;
                }
               
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

            double amountOfColumns = FloorplanGrid.ColumnDefinitions.Count;
            double amountOfRows = FloorplanGrid.RowDefinitions.Count;
            if (amountOfColumns > 0 && amountOfRows > 0)
            {
                FloorplanGrid.HeightRequest = width / amountOfColumns * amountOfRows;
            }
        }

        #region LegendGrid
        private void CreateLegendGrid()
        {
            LegendGridService.CreateLegendGridColumnDefinitions(LegendGrid);
            LegendGridService.CreateLegendGridRowDefinitions(LegendGrid, Rooms.Count);
            FillLegendGrid();
        }

        private void FillLegendGrid()
        {
            int i = 1;
            foreach (Room room in Rooms)
            {
                GridService.AddColorLabel(LegendGrid, i, 1, room.Color, false);
                GridService.AddTextLabel(LegendGrid, i, 2, room.Name, false);
                i++;
            }
        }
        #endregion

        #region FloorplanGrid
        private void CreateFloorplanGrid()
        {
            Dimensions dimensions = GridService.GetDifferenceAsDimension(Spaces);
            GridService.CreateGridColumnDefinitions(FloorplanGrid, dimensions);
            GridService.CreateGridRowDefinitions(FloorplanGrid, dimensions);
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

            GridService.AddItemToGridAtLocation(box, FloorplanGrid, row, column);
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
            TapGestureRecognizer boxTap = new TapGestureRecognizer();
            boxTap.Tapped += handler;
            box.GestureRecognizers.Add(boxTap);
        }

        private void RoomClicked(object sender, EventArgs e)
        {
            BoxView box = (BoxView)sender;
            foreach (Room r in Rooms)
            {
                if (box.BackgroundColor.Equals(r.Color))
                {
                    SelectedRoom = r;
                    TransferService.Store(SelectedRoom, Spaces);
                    break;
                }
            }
            _viewModel.RoomCommand.Execute(SelectedRoom);
        }

        private void KitchenClicked(object sender, EventArgs e)
        {
            DisplayAlert("Kitchen", "Kitchen information", "OK");
        }
        #endregion       
    }
}