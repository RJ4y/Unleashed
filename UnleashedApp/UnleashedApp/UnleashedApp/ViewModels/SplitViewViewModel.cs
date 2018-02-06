using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class SplitViewViewModel
    {
        public ICommand HomeCommand { get; set; }
        public ICommand WhoIsWhoCommand { get; set; }
        public ICommand FloorplanCommand { get; set; }

        public SplitViewViewModel()
        {
            HomeCommand = new Command(GoHome);
            WhoIsWhoCommand = new Command(GoWhoIsWho);
            FloorplanCommand = new Command(GoFloorplan);
        }

        void GoHome(object obj)
        {
            App.NavigationPage.Navigation.PopToRootAsync();
            App.MenuIsPresented = false;
        }

        void GoWhoIsWho(object obj)
        {
            App.NavigationPage.Navigation.PushAsync(new WhoIsWhoView());
            App.MenuIsPresented = false;
        }

        void GoFloorplan(object obj)
        {
            App.NavigationPage.Navigation.PushAsync(new FloorplanView());
            App.MenuIsPresented = false;
        }
    }
}
