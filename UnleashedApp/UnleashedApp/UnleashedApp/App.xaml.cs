using UnleashedApp.Authentication;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace UnleashedApp
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            if (AuthenticationService.Instance.UserIsLoggedIn())
            {
                MainPage = new NavigationPage(new MenuView());
            }
            else
            {
                MainPage = new NavigationPage(new LoginView());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}