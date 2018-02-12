﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Contracts;
using UnleashedApp.Models;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.ViewModels;
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

            var nameGamePage = new MasterItem() { Title = "Home", TargetType = typeof(NameGameView) };
            var whoIsWhoPage = new MasterItem() { Title = "Who is Who", TargetType = typeof(WhoIsWhoView) };
            var floorPlanPage = new MasterItem() { Title = "Floor Plan", TargetType = typeof(FloorplanView) };
            var trainingPage = new MasterItem() { Title = "Training", TargetType = typeof(TrainingView) };
            var aboutPage = new MasterItem() { Title = "About" };
            var logoutAction = new MasterItem() { Title = "Log out", TargetType = typeof(LoginView) };

            // Adding menu items to menuList
            menuList.Add(nameGamePage);
            menuList.Add(whoIsWhoPage);
            menuList.Add(floorPlanPage);
            menuList.Add(trainingPage);
            //menuList.Add(aboutPage);
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
                App.NavigationPage.Navigation.PopToRootAsync();
                App.MenuIsPresented = false;
            }
            else if (page == typeof(WhoIsWhoView))
            {
                App.NavigationPage.Navigation.PushAsync(new WhoIsWhoView());
                App.MenuIsPresented = false;
            }
            else if (page == typeof(FloorplanView))
            {
                App.NavigationPage.Navigation.PushAsync(new FloorplanView());
                App.MenuIsPresented = false;
            }
            else if (page == typeof(TrainingView))
            {
                App.NavigationPage.Navigation.PushAsync(new TrainingView());
                App.MenuIsPresented = false;
            }
            else if (page == typeof(LoginView))
            {
                _authenticationService.DeleteAccessTokens();
                App.NavigationPage.Navigation.PushAsync(new LoginView());
                App.MenuIsPresented = false;
            }

            ((ListView)sender).SelectedItem = null;
        }
    }
}