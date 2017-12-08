using System;
using System.Windows.Input;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class WhoIsWhoViewModel : ViewModelBase, IWhoIsWhoViewModel
    {
        //private readonly INavigationService _navigationService;

        //public ICommand HabitatCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public WhoIsWhoViewModel(INavigationService navigationService)
        {
            //InitialiseComponents(navigationService);
            //navigationService = navigationService;
            //InitialiseCommands();
        }

        /* private void InitialiseCommands()
        {
            HabitatCommand = new Command(async () =>
            {
                await NavigationService.PushAsync(nameof(MenuView));
            });
        } */
    }
}
