using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView
    {
        public MenuView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}