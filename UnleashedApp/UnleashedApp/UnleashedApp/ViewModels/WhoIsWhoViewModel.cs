using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using UnleashedApp.Repositories.HabitatRepositories;
using UnleashedApp.Repositories.SquadRepositories;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class WhoIsWhoViewModel : INotifyPropertyChanged, IWhoIsWhoViewModel
    {
        private IHabitatRepository _habitatRepository;
        private ISquadRepository _squadRepository;
        private readonly INavigationService _navigationService;
        public Habitat Habitat { get; set; }
        public List<Habitat> Habitats { get; set; }
        public List<Squad> Squads { get; set; }
        public List<Group> Groups { get; set; }
        public List<Employee> HabitatEmployeeList { get; set; }
        private ObservableCollection<Employee> _employees;
        public ICommand EmployeeDetailCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        //public List<Employee> Employees { get; set; }
        //public ObservableCollection<Employee> Employees { get; set; }
        //public ICommand HabitatCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public WhoIsWhoViewModel(INavigationService navigationService, IHabitatRepository habitatRepository, ISquadRepository squadRepository)
        {
            //InitialiseComponents(messagingCenter, navigationService);
            //InitialiseNavigation();
            //InitialiseCommands();
            _navigationService = navigationService;
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
            Employees = new ObservableCollection<Employee>(_habitatRepository.GetEmployees(1));
            foreach(Employee emp in Employees)
            {
                emp.FullName = emp.First_Name + " " + emp.Last_Name;
            }
            //Employees = _squadRepository.GetEmployees(1);
        }

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                RaisePropertyChanged(nameof(Employees));
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                RaisePropertyChanged(nameof(SelectedEmployee));
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ProvideEmployeesPerHabitat(int id)
        {
            HabitatEmployeeList = _habitatRepository.GetEmployees(id);
        }

        private void InitialiseCommands()
        {
            EmployeeDetailCommand = new Command(async () =>
            {
                MessagingCenter.Send<WhoIsWhoViewModel, Employee>(this, "", SelectedEmployee);
                await _navigationService.PushAsync(nameof(EmployeeDetailView));
            });
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
