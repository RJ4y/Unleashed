using System;
using System.Collections.Generic;
using UnleashedApp.Models;
using UnleashedApp.Services;
using UnleashedApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomView : ContentPage
    {
        private static RoomViewModel viewModel;
        public static Room Room { get; private set; }
        public static List<Space> Spaces { get; private set; }

        public RoomView()
        {
            InitializeComponent();
            viewModel = ViewModelLocator.Instance.RoomViewModel;
            Room = TransferService.GetSelectedRoom();
            Spaces = TransferService.GetSelectedSpaces();
            roomNameLabel.Text = Room.Name;
            CreateLegendGrid();
            CreateRoomGrid();
        }

        /// <summary>
        /// Makes the RoomGrid cells a square by making the width and height the correct ratio.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            double amountOfColumns = roomGrid.ColumnDefinitions.Count;
            double amountOfRows = roomGrid.RowDefinitions.Count;
            if (amountOfColumns > 0 && amountOfRows > 0)
            {
                roomGrid.HeightRequest = width / amountOfColumns * amountOfRows;
            }
        }

        #region LegendGrid
        private void CreateLegendGrid()
        {
            LegendGridService.CreateLegendGridColumnDefinitions(legendGrid);
            LegendGridService.CreateLegendGridRowDefinitions(legendGrid, 2);
            FillLegendGrid();
        }

        private void FillLegendGrid()
        {
            GridService.AddColorLabel(legendGrid, 1, 1, Color.Black, false);
            GridService.AddTextLabel(legendGrid, 1, 2, "Workspot", false);
            GridService.AddColorLabel(legendGrid, 2, 1, Room.Color, false);
            GridService.AddTextLabel(legendGrid, 2, 2, "Empty", false);
        }
        #endregion

        #region RoomGrid
        private void CreateRoomGrid()
        {
            Dimensions dimensions = GridService.GetMinifiedSquareGridDimensions(Spaces);
            GridService.CreateGridColumnDefinitions(roomGrid, dimensions);
            GridService.CreateGridRowDefinitions(roomGrid, dimensions);
            FillRoomGrid();
        }

        private void FillRoomGrid()
        {
            Dimensions translation = GridService.GetGridTranslation(Spaces);

            foreach (Space space in Spaces)
            {
                if (space.EmployeeId == null)
                {
                    AddEmptyCell(space.XCoord - translation.X, space.YCoord - translation.Y, Room.Color);
                }
                else
                {
                    AddEmployeeWorkspotCell(space.XCoord - translation.X, space.YCoord - translation.Y, Room);
                }
            }
        }

        private void AddEmptyCell(int column, int row, Color color)
        {
            BoxView box = new BoxView { BackgroundColor = color };
            GridService.AddItemToGridAtLocation(box, roomGrid, row, column);
        }

        private void AddEmployeeWorkspotCell(int column, int row, Room room)
        {
            BoxView box = new BoxView { BackgroundColor = Color.Black };
            AddWorkspaceTapEventToBox(box);
            GridService.AddItemToGridAtLocation(box, roomGrid, row, column);
        }

        private void AddWorkspaceTapEventToBox(BoxView box)
        {
            TapGestureRecognizer box_tap = new TapGestureRecognizer();
            box_tap.Tapped += WorkspaceClicked;
            box.GestureRecognizers.Add(box_tap);
        }

        private void WorkspaceClicked(object sender, EventArgs e)
        {
            BoxView box = ((BoxView)sender);

            Dimensions translation = GridService.GetGridTranslation(Spaces);
            int x = Grid.GetRow(box) + translation.X;
            int y = Grid.GetColumn(box) + translation.Y;

            foreach (Space space in Spaces)
            {
                if (space.XCoord == x && space.YCoord == y)
                {
                    ShowEmployeeInformation(space);
                }
            }
        }

        private async void ShowEmployeeInformation(Space space)
        {
            RoomViewModel vm = ViewModelLocator.Instance.RoomViewModel;
            Employee e = vm.EmployeeRepository.GetEmployeeById(space.EmployeeId);
            ViewModelLocator.Instance.RoomViewModel.SelectedEmployee = e;

            bool action = await DisplayAlert(e.FirstName + " " + e.LastName, e.Function, "View details", "Close");
            if (action == true)
            {
                vm.EmployeeDetailCommand.Execute(null);
            }
        }
        #endregion
    }
}