using UnleashedApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace UnleashedApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //AppContainer.Container = new Bootstrap().CreateContainer();

            //var menuView = new MenuView();
            //var whoisWhoView = new WhoIsWhoView();

            //var nagigationService = AppContainer.Container.Resolve<INavigationService>();
            //nagigationService.NavigationContext = menuView.Navigation;

            //MainPage = new NavigationPage(new MenuView());

            MainPage = new NavigationPage(new LoginView());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
