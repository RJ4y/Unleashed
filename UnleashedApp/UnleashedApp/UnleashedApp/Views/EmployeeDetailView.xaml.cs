using System.Collections.Generic;
using UnleashedApp.Models;
using UnleashedApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeDetailView
    {
        public static List<Space> Spaces { get; private set; }
        public static List<Room> Rooms { get; private set; }
        public static Employee Employee;
        private bool shouldLoad = true;

        public EmployeeDetailView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.EmployeeDetailViewModel;
            Employee = TransferService.GetSelectedEmployee();

            Rooms = ViewModelLocator.Instance.EmployeeDetailViewModel.Rooms;
            Spaces = ViewModelLocator.Instance.EmployeeDetailViewModel.Spaces;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext != null)
            {
                ViewModelLocator.Instance.EmployeeDetailViewModel.LoadSpaces();
                ViewModelLocator.Instance.EmployeeDetailViewModel.LoadRooms();
                Rooms = ViewModelLocator.Instance.EmployeeDetailViewModel.Rooms;
                Spaces = ViewModelLocator.Instance.EmployeeDetailViewModel.Spaces;
            }
            if (shouldLoad && Spaces != null && Spaces.Count > 0 && Employee != null && Rooms != null && Rooms.Count > 0)
                CreateLocationGrid();
            shouldLoad = false;
        }

        private void CreateLocationGrid()
        {
            Dimensions dimensions = GridService.GetDifferenceAsDimension(Spaces);
            GridService.CreateGridColumnDefinitions(LocationGrid, dimensions);
            GridService.CreateGridRowDefinitions(LocationGrid, dimensions);
            FillLocationGrid();
        }

        private void FillLocationGrid()
        {
            foreach (Space space in Spaces)
                AddCell(space.XCoord, space.YCoord, space.EmployeeId, Rooms.Find(r => r.Id == space.RoomId));
        }

        private void AddCell(int column, int row, int? employeeId, Room room)
        {
            BoxView box;
            if (employeeId == Employee.Id)
                box = new BoxView { BackgroundColor = Color.Black };
            else
                box = new BoxView { BackgroundColor = room.Color };
            GridService.AddItemToGridAtLocation(box, LocationGrid, row, column);
        }

        /// <summary>
        /// Makes the FloorplanGrid cells a square by making the width and height the correct ratio.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            double amountOfColumns = LocationGrid.ColumnDefinitions.Count;
            double amountOfRows = LocationGrid.RowDefinitions.Count;
            if (amountOfColumns > 0 && amountOfRows > 0)
            {
                LocationGrid.HeightRequest = width / amountOfColumns * amountOfRows;
            }
        }
    }
}