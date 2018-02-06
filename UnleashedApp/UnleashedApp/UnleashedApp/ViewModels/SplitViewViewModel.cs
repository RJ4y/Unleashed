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
        private readonly INavigationService _navigationService;

        public ICommand GoHomeCommand { get; set; }
        public ICommand GoWhoIsWhoCommand { get; set; }
        public ICommand GoFloorplanCommand { get; set; }

        public SplitViewViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            GoHomeCommand = new Command(() =>
            {
                App.NavigationPage.Navigation.PopToRootAsync();
                App.MenuIsPresented = false;
            });
            GoWhoIsWhoCommand = new Command(() =>
            {
                App.NavigationPage.Navigation.PushAsync(new WhoIsWhoView());
                App.MenuIsPresented = false;
            });
            GoFloorplanCommand = new Command(() =>
            {
                App.NavigationPage.Navigation.PushAsync(new FloorplanView());
                App.MenuIsPresented = false;
            });
        }
    }
}
