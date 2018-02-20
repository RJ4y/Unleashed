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
    public partial class RoomView
    {
        public static Room Room { get; private set; }
        public static List<Space> Spaces { get; private set; }

        public RoomView()
        {
            InitializeComponent();
            Room = TransferService.GetSelectedRoom();
            Spaces = TransferService.GetSelectedSpaces();
            RoomNameLabel.Text = Room.Name;
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

            double amountOfColumns = RoomGrid.ColumnDefinitions.Count;
            double amountOfRows = RoomGrid.RowDefinitions.Count;
            if (amountOfColumns > 0 && amountOfRows > 0)
            {
                RoomGrid.HeightRequest = width / amountOfColumns * amountOfRows;
            }
        }

        #region LegendGrid
        private void CreateLegendGrid()
        {
            LegendGridService.CreateLegendGridColumnDefinitions(LegendGrid);
            LegendGridService.CreateLegendGridRowDefinitions(LegendGrid, 2);
            FillLegendGrid();
        }

        private void FillLegendGrid()
        {
            GridService.AddColorLabel(LegendGrid, 1, 1, Color.Black, false);
            GridService.AddTextLabel(LegendGrid, 1, 2, "Workspot", false);
            GridService.AddColorLabel(LegendGrid, 2, 1, Room.Color, false);
            GridService.AddTextLabel(LegendGrid, 2, 2, "Empty", false);
        }
        #endregion

        #region RoomGrid
        private void CreateRoomGrid()
        {
            Dimensions dimensions = GridService.GetMinifiedSquareGridDimensions(Spaces);
            GridService.CreateGridColumnDefinitions(RoomGrid, dimensions);
            GridService.CreateGridRowDefinitions(RoomGrid, dimensions);
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
                    AddEmployeeWorkspotCell(space.XCoord - translation.X, space.YCoord - translation.Y);
                }
            }
        }

        private void AddEmptyCell(int column, int row, Color color)
        {
            BoxView box = new BoxView { BackgroundColor = color };
            GridService.AddItemToGridAtLocation(box, RoomGrid, row, column);
        }

        private void AddEmployeeWorkspotCell(int column, int row)
        {
            BoxView box = new BoxView { BackgroundColor = Color.Black };
            AddWorkspaceTapEventToBox(box);
            GridService.AddItemToGridAtLocation(box, RoomGrid, row, column);
        }

        private void AddWorkspaceTapEventToBox(BoxView box)
        {
            TapGestureRecognizer boxTap = new TapGestureRecognizer();
            boxTap.Tapped += WorkspaceClicked;
            box.GestureRecognizers.Add(boxTap);
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
                    break;
                }
            }
        }

        private async void ShowEmployeeInformation(Space space)
        {
            Employee e = ViewModelLocator.Instance.RoomViewModel.EmployeeRepository.GetEmployeeById(space.EmployeeId);
            ViewModelLocator.Instance.RoomViewModel.SelectedEmployee = e;

            bool action = await DisplayAlert(e.FirstName + " " + e.LastName, e.Function, "View details", "Close");
            if (action)
            {
                TransferService.Store(e);
                ViewModelLocator.Instance.RoomViewModel.EmployeeDetailCommand.Execute(null);
            }
        }
        #endregion
    }
}