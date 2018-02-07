using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Models;
using UnleashedApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplitViewView : ContentPage
    {

        public SplitViewView()
        {
            Title = "Unleashed";
            InitializeComponent();

            List<MasterItem> menuList = new List<MasterItem>();

            var homePage = new MasterItem() { Title = "Home", TargetType = typeof(MenuView) };
            var whoIsWhoPage = new MasterItem() { Title = "Who is Who", TargetType = typeof(WhoIsWhoView) };
            var nameGamePage = new MasterItem() { Title = "Name Game", TargetType = typeof(NameGameView) };
            var floorPlanPage = new MasterItem() { Title = "Floor plan", TargetType = typeof(FloorplanView) };
            var trainingPage = new MasterItem() { Title = "Training" };
            var aboutPage = new MasterItem() { Title = "About" };
            var logoutAction = new MasterItem() { Title = "Log out" };

            // Adding menu items to menuList
            menuList.Add(homePage);
            menuList.Add(whoIsWhoPage);
            menuList.Add(nameGamePage);
            menuList.Add(floorPlanPage);
            menuList.Add(trainingPage);
            menuList.Add(aboutPage);
            menuList.Add(logoutAction);

            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;
        }

        private void OnMasterTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (MasterItem) e.Item;
            Type page = item.TargetType;

            if (page == typeof(MenuView))
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
            else if (page == typeof(NameGameView))
            {
                App.NavigationPage.Navigation.PushAsync(new NameGameView());
                App.MenuIsPresented = false;
            }

            ((ListView)sender).SelectedItem = null;
        }
    }
}