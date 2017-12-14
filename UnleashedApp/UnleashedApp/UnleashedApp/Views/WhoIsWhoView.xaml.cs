using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhoIsWhoView : ContentPage
    {
        //private IWhoIsWhoViewModel _viewModel;

        public WhoIsWhoView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //_viewModel = AppContainer.Container.Resolve<IWhoIsWhoViewModel>();
            //BindingContext = _viewModel;
        }
    }
}