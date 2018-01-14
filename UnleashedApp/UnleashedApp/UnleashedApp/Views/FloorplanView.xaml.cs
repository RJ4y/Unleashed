using System;
using System.Collections.Generic;
using UnleashedApp.Models;
using UnleashedApp.Services;
using UnleashedApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static UnleashedApp.Models.DrawableRoom;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloorplanView : ContentPage
    {
        private static List<Space> spaces;

        public FloorplanView()
        {
            InitializeComponent();
            GridService.CreateRooms();
            CreateSpaces();
            CreateLegendGrid();
            CreateFloorplanGrid();
        }

        private void CreateSpaces()
        {
            spaces = new List<Space>
            {
                new Space(0,0,0,1),
                new Space(1,0,0,1),
                new Space(2,0,0,1),
                new Space(3,0,0,3),
                new Space(4,0,1,3),
                new Space(0,1,0,2),
                new Space(1,1,0,2),
                new Space(2,1,0,2),
                new Space(3,1,0,3),
                new Space(4,1,0,3),
                new Space(0,2,0,2),
                new Space(1,2,0,2),
                new Space(2,2,0,2),
                new Space(3,2,0,3),
                new Space(4,2,0,3),
                new Space(0,3,0,2),
                new Space(1,3,0,2),
                new Space(2,3,0,2),
                new Space(3,3,0,3),
                new Space(4,3,2,3)
            };
        }

        #region LegendGrid
        private void CreateLegendGrid()
        {
            GridService.CreateLegendGridColumnDefinitions(LegendGrid);
            GridService.CreateLegendGridRowDefinitions(LegendGrid, GridService.Rooms.Count);
            FillLegendGrid();
        }

        private void FillLegendGrid()
        {
            int i = 1;
            foreach (DrawableRoom room in GridService.Rooms)
            {
                GridService.AddColorLabel(LegendGrid, i, 1, room.Color);
                GridService.AddTextLabel(LegendGrid, i, 2, room.Name);
                i++;
            }
        }
        #endregion

        #region FloorplanGrid
        private void CreateFloorplanGrid()
        {
            Size size = new Size(10, 10);
            GridService.CreateGridColumnDefinitions(FloorplanGrid, size);
            GridService.CreateGridRowDefinitions(FloorplanGrid, size);
            FillFloorplanGrid();
        }

        private void FillFloorplanGrid()
        {
            foreach (Space space in spaces)
            {
                AddCell(space.XCoord, space.YCoord, GridService.Rooms.Find(r => r.Id == space.RoomId));
            }
        }

        private void AddCell(int column, int row, DrawableRoom room)
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
            TapGestureRecognizer box_tap = new TapGestureRecognizer();
            box_tap.Tapped += handler;
            box.GestureRecognizers.Add(box_tap);
        }

        private void RoomClicked(object sender, EventArgs e)
        {
            int id = -1;
            BoxView box = ((BoxView)sender);
            foreach (DrawableRoom r in GridService.Rooms)
            {
                if (box.BackgroundColor.Equals(r.Color))
                {
                    id = r.Id;
                }
            }
            if (id != -1)
            {
                GridService.StoreSpacesFromSelectedRoom(id, spaces);
                FloorplanViewModel vm = ViewModelLocator.Instance.FloorplanViewModel;
                vm.RoomCommand.Execute(null);
            }
        }

        private void KitchenClicked(object sender, EventArgs e)
        {
            BoxView box = ((BoxView)sender);
            DisplayAlert("Kitchen", "Kitchen information", "OK");
        }
        #endregion       
    }
}