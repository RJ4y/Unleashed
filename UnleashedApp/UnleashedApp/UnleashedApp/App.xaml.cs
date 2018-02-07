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
            MainPage = new NavigationPage(new MenuView());
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