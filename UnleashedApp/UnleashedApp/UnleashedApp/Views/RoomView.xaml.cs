using System;
using UnleashedApp.Models;
using UnleashedApp.Services;
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
            GridService.AddColorLabel(LegendGrid, 2, 1, GridService.GetRoom().Color);
            GridService.AddTextLabel(LegendGrid, 2, 2, "Empty");
        }
        #endregion

        #region RoomGrid
        private void CreateRoomGrid()
        {
            Size dimensions = GridService.GetRoomGridDimensions();
            GridService.CreateGridColumnDefinitions(RoomGrid, dimensions);
            GridService.CreateGridRowDefinitions(RoomGrid, dimensions);
            FillRoomGrid();
        }

        private void FillRoomGrid()
        {
            Size translation = GridService.GetRoomGridTranslation();
            int xChange = Convert.ToInt32(translation.Width);
            int yChange = Convert.ToInt32(translation.Height);
            foreach (Space space in GridService.SelectedSpaces)
            {
                if (space.EmployeeId == 0)
                {
                    AddEmptyCell(space.XCoord - xChange, space.YCoord - yChange, GridService.GetRoom().Color);
                }
                else
                {
                    AddEmployeeWorkspotCell(space.XCoord - xChange, space.YCoord - yChange, GridService.GetRoom());
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

            Size translation = GridService.GetRoomGridTranslation();
            int x = Grid.GetColumn(box) + Convert.ToInt32(translation.Width);
            int y = Grid.GetRow(box) + Convert.ToInt32(translation.Height);

            foreach (Space space in GridService.SelectedSpaces)
            {
                if (space.XCoord == x && space.YCoord == y)
                {
                    DisplayAlert("Employee " + space.EmployeeId, "Employee information", "OK");
                }
            }
        }
        #endregion
    }
}