using UnleashedApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhoIsWhoView: TabbedPage
    {
        public WhoIsWhoView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.WhoIsWhoViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext != null)
            {
                ((WhoIsWhoViewModel)BindingContext).LoadEmployeesPerHabitat();
                ((WhoIsWhoViewModel)BindingContext).RefreshFilteredHabitatList();
                ((WhoIsWhoViewModel)BindingContext).RefreshFilteredSquadList();
                ((WhoIsWhoViewModel)BindingContext).LoadEmployeesPerSquad();
            }
        }
    }
}