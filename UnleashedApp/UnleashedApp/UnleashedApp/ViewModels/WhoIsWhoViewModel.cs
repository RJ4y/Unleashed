using System.Collections.Generic;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class WhoIsWhoViewModel : ViewModelBase, IWhoIsWhoViewModel
    {
        //public ICommand HabitatCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public WhoIsWhoViewModel(IMessagingCenter messagingCenter, INavigationService navigationService)
        {
            InitialiseComponents(messagingCenter, navigationService);
            //InitialiseNavigation();
            //InitialiseCommands();
        }

        //get all habitats
        //get all employees where e.habitat_id == h.id


             
        /* private void InitialiseNavigation()
        {
            MessagingCenter.Subscribe<MenuViewModel, CurrentUser>(this, Constants.Values.SHOW_QR_NAVIGATION_LINK, (sender, data) =>
            {
                User = data;
                Contents = CreateImageSourceFromId();
            });
        } */

        /* private void InitialiseCommands()
        {
            HabitatCommand = new Command(async () =>
            {
                await NavigationService.PushAsync(nameof(MenuView));
            });
        } */
    }
}
