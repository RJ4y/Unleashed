using UnleashedApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace UnleashedApp
{
    public partial class App
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

            RootPage = new RootPage()
            {
                Master = splitView,
                Detail = NavigationPage
            };
            MainPage = RootPage;
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