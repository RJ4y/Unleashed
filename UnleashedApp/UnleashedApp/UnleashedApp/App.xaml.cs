using UnleashedApp.Authentication;
using UnleashedApp.Contracts;
using UnleashedApp.Repositories;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace UnleashedApp
{
    public partial class App
    {
        public static NavigationPage NavigationPage { get; set; }
        public static RootPage RootPage;

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
            
            IAuthenticationService authenticationService = AuthenticationService.Instance;
            IHttpClientAdapter httpClientAdapter = new HttpClientAdapter();
            IAuthenticationRepository authenticationRepository = new AuthenticationRepository(authenticationService, httpClientAdapter, new AuthenticationHttpClientAdapter(authenticationService, httpClientAdapter));

            var splitView = new SplitViewView(authenticationService, authenticationRepository);
            
            if (AuthenticationService.Instance.UserIsLoggedIn())
            {
                NavigationPage = new NavigationPage(new NameGameView());
            }
            else
            {
                NavigationPage = new NavigationPage(new LoginView());
            }

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