using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using UnleashedApp.Repositories.EmployeeRepositories;
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
        private IEmployeeRepository _employeeRepository;
        private readonly INavigationService _navigationService;
        public Habitat Habitat { get; set; }
        public List<Habitat> Habitats { get; set; }
        public List<Squad> Squads { get; set; }
        public List<Group> Groups { get; set; }
        public List<Employee> HabitatEmployeeList { get; set; }
        private ObservableCollection<Employee> _employees;
        public ICommand EmployeeDetailCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public WhoIsWhoViewModel(INavigationService navigationService, IHabitatRepository habitatRepository, ISquadRepository squadRepository, IEmployeeRepository employeeRepository)
        {
            _navigationService = navigationService;
            _habitatRepository = habitatRepository;
            _squadRepository = squadRepository;
            _employeeRepository = employeeRepository;
            InitialiseCommands();
            LoadData();
            
        }

        public void LoadData()
        {
            Employees = new ObservableCollection<Employee>(_employeeRepository.GetAllEmployees());
            foreach(Employee emp in Employees)
            {
                emp.FullName = emp.First_Name + " " + emp.Last_Name;
            }
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
                //MessagingCenter.Send<WhoIsWhoViewModel, Employee>(this, "", SelectedEmployee);           
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
                //MessagingCenter.Send<WhoIsWhoViewModel, Employee>(this, "", SelectedEmployee);
                await _navigationService.PushAsync(nameof(EmployeeDetailView));
            });
        }
    }
}
