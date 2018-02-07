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
        }

        private void SquadButtonClicked(object sender, EventArgs e)
        {
        }
    }
}