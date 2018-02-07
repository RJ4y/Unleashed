using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhoIsWhoView
    {
        public WhoIsWhoView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.WhoIsWhoViewModel;
        }

        void HabitatButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            button.BackgroundColor = Color.DodgerBlue;
            button.TextColor = Color.White;
            SquadButton.BackgroundColor = Color.White;
            SquadButton.TextColor = Color.DodgerBlue;
        }

        private void SquadButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundColor = Color.DodgerBlue;
            button.TextColor = Color.White;
            HabitatButton.BackgroundColor = Color.White;
            HabitatButton.TextColor = Color.DodgerBlue;
        }
    }
}