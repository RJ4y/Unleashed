using System;
using System.Collections.Generic;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using UnleashedApp.Repositories.HabitatRepositories;
using UnleashedApp.Repositories.SquadRepositories;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class WhoIsWhoViewModel : ViewModelBase, IWhoIsWhoViewModel
    {
        private IHabitatRepository _habitatRepository;
        private ISquadRepository _squadRepository;
        public Habitat Habitat { get; set; }
        public List<Habitat> Habitats { get; set; }
        public List<Squad> Squads { get; set; }
        public List<String> Groups { get; set; }
        //public ICommand HabitatCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public WhoIsWhoViewModel(IMessagingCenter messagingCenter, INavigationService navigationService, IHabitatRepository habitatRepository, ISquadRepository squadRepository)
        {
            InitialiseComponents(messagingCenter, navigationService);
            //InitialiseNavigation();
            //InitialiseCommands();
            _habitatRepository = habitatRepository;
            _squadRepository = squadRepository;
            loadData();
        }

        public void loadData()
        {
            Habitats = _habitatRepository.GetAllHabitats();
            Squads = _squadRepository.GetAllSquads();

            foreach(Habitat habitat in Habitats)
            {
                Groups.Add(habitat.Name);
            }
        }

             
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
