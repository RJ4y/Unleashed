using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoomView : ContentPage
	{
		public RoomView ()
		{
			InitializeComponent ();
            FillGrid();
        }

        private void FillGrid()
        {
            AddBox(0, 0, Color.LightGray);
            AddBox(0, 1, Color.LightGray);

            AddBox(1, 0, Color.LightGray);
            AddBox(1, 1, Color.LightGray);

            AddBox(2, 0, Color.LightGray);
            AddBox(2, 1, Color.LightGray);

            AddBox(3, 0, Color.LightGray);
            AddBox(3, 1, Color.LightGray);
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
            RoomGrid.Children.Add(box);
        }

        private void AddTapEventToBox(BoxView box)
        {
            TapGestureRecognizer box_tap = new TapGestureRecognizer();
            box_tap.Tapped += WorkspaceClicked;
            box.GestureRecognizers.Add(box_tap);
        }

        private void WorkspaceClicked(object sender, EventArgs e)
        {
            BoxView box = ((BoxView)sender);
            DisplayAlert("Alert", "You have pressed workspace " + Grid.GetColumn(box) + "," + Grid.GetRow(box), "OK");
        }
    }
}