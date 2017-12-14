using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : ContentPage
    {
        //private readonly IMenuViewModel _viewModel;

        public MenuView()
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
            //_viewModel = AppContainer.Container.Resolve<IMenuViewModel>();
            //BindingContext = _viewModel;
        }
    }
}