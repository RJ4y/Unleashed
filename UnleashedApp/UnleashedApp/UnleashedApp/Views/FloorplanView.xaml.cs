using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloorplanView : ContentPage
    {
        static int gridWidth = 10;
        static int gridHeight = 10;
        public FloorplanView()
        {
            InitializeComponent();
            CreateLegendGrid();
            FillGrid();
        }

        private void CreateLegendGrid()
        {
            Dictionary<string, Color> rooms = new Dictionary<string, Color> {
             {"Hallway",Color.Blue},
             {"Kitchen", Color.Green},
             {"Workspace",Color.Black}
            };
            LegendGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            LegendGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
            LegendGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(88, GridUnitType.Star) });
            LegendGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(6, GridUnitType.Star) });

            for (int i = 0; i < rooms.Count; i++)
            {
                LegendGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }


            AddColorLabel(1, 1, Color.Blue);
            AddColorLabel(2, 1, Color.Green);
            AddColorLabel(3, 1, Color.Black);
            AddTextLabel(1, 2, "Hallway");
            AddTextLabel(2, 2, "Kitchen");
            AddTextLabel(3, 2, "Workspace");
        }

        private void FillGrid()
        {
            AddBox(0, 0, Color.Blue);
            AddBox(0, 1, Color.Blue);
            AddBox(0, 2, Color.Blue);
            AddBox(0, 3, Color.Black);
            AddBox(0, 4, Color.Black);

            AddBox(1, 0, Color.Green);
            AddBox(1, 1, Color.Green);
            AddBox(1, 2, Color.Green);
            AddBox(1, 3, Color.Black);
            AddBox(1, 4, Color.Black);

            AddBox(2, 0, Color.Green);
            AddBox(2, 1, Color.Green);
            AddBox(2, 2, Color.Green);
            AddBox(2, 3, Color.Black);
            AddBox(2, 4, Color.Black);

            AddBox(3, 0, Color.Green);
            AddBox(3, 1, Color.Green);
            AddBox(3, 2, Color.Green);
            AddBox(3, 3, Color.Black);
            AddBox(3, 4, Color.Black);
        }

        private void AddColorLabel(int row, int column, Color color)
        {
            Label label = new Label();
            label.BackgroundColor = color;
            Grid.SetRow(label, row);
            Grid.SetColumn(label, column);
            LegendGrid.Children.Add(label);
        }

        private void AddTextLabel(int row, int column, string text)
        {
            Label label = new Label();
            label.Text = text;
            Grid.SetRow(label, row);
            Grid.SetColumn(label, column);
            LegendGrid.Children.Add(label);
        }

        private void AddBox(int row, int column, Color color)
        {
            BoxView box = new BoxView();
            box.BackgroundColor = color;

            if (box.BackgroundColor == Color.Black)
            {
                AddTapEventToBox(box);
            }

            Grid.SetRow(box, row);
            Grid.SetColumn(box, column);
            FloorplanGrid.Children.Add(box);
        }

        private void AddTapEventToBox(BoxView box)
        {
            TapGestureRecognizer box_tap = new TapGestureRecognizer();
            box_tap.Tapped += RoomClicked;
            box.GestureRecognizers.Add(box_tap);
        }

        private void RoomClicked(object sender, EventArgs e)
        {
            BoxView box = ((BoxView)sender);
            var vm = ViewModelLocator.Instance.FloorplanViewModel;
            vm.RoomCommand.Execute(null);
        }
    }
}