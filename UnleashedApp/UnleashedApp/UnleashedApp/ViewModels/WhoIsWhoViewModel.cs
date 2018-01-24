﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public List<Group> Groups { get; set; }
        public List<Employee> HabitatEmployeeList { get; set; }
        //public List<Employee> Employees { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        //public ICommand HabitatCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public WhoIsWhoViewModel(INavigationService navigationService, IHabitatRepository habitatRepository, ISquadRepository squadRepository)
        {
            //InitialiseComponents(messagingCenter, navigationService);
            //InitialiseNavigation();
            //InitialiseCommands();
            _habitatRepository = habitatRepository;
            _squadRepository = squadRepository;
            LoadData();
        }

        public void LoadData()
        {
            /*Habitats = _habitatRepository.GetAllHabitats();
            Squads = _squadRepository.GetAllSquads();

            foreach (Habitat habitat in Habitats)
            {
                Groups.Add(habitat);
            } */
            if(_habitatRepository.GetEmployees(1) != null)
            {
                Employees = new ObservableCollection<Employee>(_habitatRepository.GetEmployees(1));
                Debug.WriteLine("****************************************************** " + Employees[0].First_Name);
            }

            //Employees = _squadRepository.GetEmployees(1);
        }

        public void ProvideEmployeesPerHabitat(int id)
        {
            HabitatEmployeeList = _habitatRepository.GetEmployees(id);
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
