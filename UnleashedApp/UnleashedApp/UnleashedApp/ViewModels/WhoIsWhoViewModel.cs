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
        private ObservableCollection<Employee> _employees;
        private ObservableCollection<Group> _groupedList;
        public ICommand EmployeeDetailCommand { get; set; }
        public ICommand HabitatCommand { get; set; }
        public ICommand SquadCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public WhoIsWhoViewModel(INavigationService navigationService, IHabitatRepository habitatRepository, ISquadRepository squadRepository, IEmployeeRepository employeeRepository)
        {
            _navigationService = navigationService;
            _habitatRepository = habitatRepository;
            _squadRepository = squadRepository;
            _employeeRepository = employeeRepository;
            InitialiseCommands();
            LoadHabitats();
            //LoadSquads();
        }

        public void LoadData()
        {
            Employees = new ObservableCollection<Employee>(_employeeRepository.GetAllEmployees());
            foreach(Employee emp in Employees)
            {
                emp.FullName = emp.First_Name + " " + emp.Last_Name;
            }
        }

        public void LoadHabitats()
        {
            var habitats = _habitatRepository.GetAllHabitats();
            GroupedList = new ObservableCollection<Group>();

            foreach(Habitat habitat in habitats)
            {
                var group = new Group
                {
                    Id = habitat.Id,
                    Name = habitat.Name
                };

                GroupedList.Add(group);
            }

            foreach (Group group in GroupedList)
            {
                var employees = _habitatRepository.GetEmployees(group.Id);

                foreach(Employee employee in employees)
                {
                    employee.FullName = employee.First_Name + " " + employee.Last_Name;
                    group.Add(employee);
                }
            }
        }

        public void LoadSquads()
        {
            var squads = _squadRepository.GetAllSquads();
            GroupedList = new ObservableCollection<Group>();

            foreach (Squad squad in squads)
            {
                var group = new Group
                {
                    Id = squad.Id,
                    Name = squad.Name
                };

                GroupedList.Add(group);
            }

            foreach (Group group in GroupedList)
            {
                var employees = _squadRepository.GetEmployees(group.Id);

                foreach (Employee employee in employees)
                {
                    employee.FullName = employee.First_Name + " " + employee.Last_Name;
                    group.Add(employee);
                }
            }
        }

        public ObservableCollection<Group> GroupedList
        {
            get => _groupedList;
            set
            {
                _groupedList = value;
                RaisePropertyChanged(nameof(GroupedList));
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
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitialiseCommands()
        {
            EmployeeDetailCommand = new Command(async () =>
            {
                var empDetailPage = new EmployeeDetailView();
                empDetailPage.BindingContext = SelectedEmployee;

                await _navigationService.PushAsync(empDetailPage);
                SelectedEmployee = null;
            });
            HabitatCommand = new Command(() =>
            {
                LoadHabitats();
            });
            SquadCommand = new Command(() =>
            {
                LoadSquads();
            });
        }
    }
}
