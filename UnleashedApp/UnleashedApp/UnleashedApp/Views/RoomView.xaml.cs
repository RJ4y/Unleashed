using System;
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
        public RoomView()
        {
            InitializeComponent();
            CreateLegendGrid();
            CreateRoomGrid();
        }

        /// <summary>
        /// Makes the RoomGrid a square
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
            GridService.CreateLegendGridColumnDefinitions(LegendGrid);
            GridService.CreateLegendGridRowDefinitions(LegendGrid, 2);
            FillLegendGrid();
        }

        private void FillLegendGrid()
        {
            GridService.AddColorLabel(LegendGrid, 1, 1, Color.Black);
            GridService.AddTextLabel(LegendGrid, 1, 2, "Workspot");
            GridService.AddColorLabel(LegendGrid, 2, 1, GridService.GetSelectedRoom().Color);
            GridService.AddTextLabel(LegendGrid, 2, 2, "Empty");
        }
        #endregion

        #region RoomGrid
        private void CreateRoomGrid()
        {
            Dimension dimensions = GridService.GetRoomGridDimensions();
            GridService.CreateGridColumnDefinitions(RoomGrid, dimensions);
            GridService.CreateGridRowDefinitions(RoomGrid, dimensions);
            FillRoomGrid();
        }

        private void FillRoomGrid()
        {
            Dimension translation = GridService.GetRoomGridTranslation();

            foreach (Space space in GridService.SelectedSpaces)
            {
                if (space.EmployeeId == 0)
                {
                    AddEmptyCell(space.XCoord - translation.X, space.YCoord - translation.Y, GridService.GetSelectedRoom().Color);
                }
                else
                {
                    AddEmployeeWorkspotCell(space.XCoord - translation.X, space.YCoord - translation.Y, GridService.GetSelectedRoom());
                }
            }
        }

        private void AddEmptyCell(int column, int row, Color color)
        {
            BoxView box = new BoxView { BackgroundColor = color };
            GridService.AddItemToGridAtLocation(box, RoomGrid, row, column);
        }

        private void AddEmployeeWorkspotCell(int column, int row, DrawableRoom room)
        {
            BoxView box = new BoxView { BackgroundColor = Color.Black };
            AddWorkspaceTapEventToBox(box);
            GridService.AddItemToGridAtLocation(box, RoomGrid, row, column);
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

            Dimension translation = GridService.GetRoomGridTranslation();
            int x = Grid.GetColumn(box) + translation.X;
            int y = Grid.GetRow(box) + translation.Y;

            foreach (Space space in GridService.SelectedSpaces)
            {
                if (space.XCoord == x && space.YCoord == y)
                {
                    ShowEmployeeInformation(space);
                }
            }
        }

        private async void ShowEmployeeInformation(Space space)
        {
            bool action = await DisplayAlert("Employee " + space.EmployeeId, "Employee information", "View details", "Close");
            if (action == true)
            {
                RoomViewModel vm = ViewModelLocator.Instance.RoomViewModel;
                vm.WhoIsWhoCommand.Execute(space.EmployeeId);
            }
        }
        #endregion
    }
}