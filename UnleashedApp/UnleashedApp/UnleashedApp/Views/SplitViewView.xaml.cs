using System;
using System.Collections.Generic;
using UnleashedApp.Contracts;
using UnleashedApp.Models;
using UnleashedApp.Repositories.AuthenticationRepositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplitViewView : ContentPage
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthenticationRepository _authenticationRepository;

        public SplitViewView(IAuthenticationService authenticationService, IAuthenticationRepository authenticationRepository)
        {
            Title = "Unleashed";
            InitializeComponent();

            _authenticationRepository = authenticationRepository;
            _authenticationService = authenticationService;

            List<MasterItem> menuList = new List<MasterItem>();
            List<MasterItem> logoutList = new List<MasterItem>();
            
            var whoIsWhoPage = new MasterItem() { Title = "Who is who", Icon = "users.png", TargetType = typeof(WhoIsWhoView) };
            var nameGamePage = new MasterItem() { Title = "Guess who", Icon = "home.png", TargetType = typeof(NameGameView) };
            var floorPlanPage = new MasterItem() { Title = "Floor plan", Icon = "map.png", TargetType = typeof(FloorplanView) };
            var trainingPage = new MasterItem() { Title = "Training", Icon = "training.png", TargetType = typeof(TrainingView) };
            var logoutAction = new MasterItem() { Title = "Log out", Icon = "signout.png", TargetType = typeof(LoginView) };

            // Adding menu items to menuList
            menuList.Add(nameGamePage);
            menuList.Add(whoIsWhoPage);
            menuList.Add(floorPlanPage);
            menuList.Add(trainingPage);
            menuList.Add(logoutAction);

            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;
        }

        private void OnMasterTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (MasterItem) e.Item;
            Type page = item.TargetType;
            
            if (page == typeof(NameGameView))
            {
                App.NavigationPage.Navigation.PushAsync(new NameGameView());
            }
            else if (page == typeof(WhoIsWhoView))
            {
                App.NavigationPage.Navigation.PushAsync(new WhoIsWhoView());
            }
            else if (page == typeof(FloorplanView))
            {
                App.NavigationPage.Navigation.PushAsync(new FloorplanView());
            }
            else if (page == typeof(TrainingView))
            {
                App.NavigationPage.Navigation.PushAsync(new TrainingView());
            }
            else if (page == typeof(LoginView))
            {
                _authenticationService.DeleteAccessTokens();
                App.NavigationPage.Navigation.PushAsync(new LoginView());
            }

            App.MenuIsPresented = false;
            App.NavigationPage.Navigation.RemovePage(App.NavigationPage.Navigation.NavigationStack[0]);
        }
    }
}