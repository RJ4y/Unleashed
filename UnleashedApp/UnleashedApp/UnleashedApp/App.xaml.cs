using UnleashedApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace UnleashedApp
{
    public partial class App : Application
    {
        public static NavigationPage NavigationPage { get; set; }
        private static RootPage RootPage;

        public static bool MenuIsPresented
        {
            get
            {
                return RootPage.IsPresented;
            }
            set
            {
                RootPage.IsPresented = value;
            }
        }

        public App()
        {
            InitializeComponent();

            var splitView = new SplitViewView();
            NavigationPage = new NavigationPage(new NameGameView());

            RootPage = new RootPage();
            RootPage.Master = splitView;
            RootPage.Detail = NavigationPage;
            MainPage = RootPage;
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
