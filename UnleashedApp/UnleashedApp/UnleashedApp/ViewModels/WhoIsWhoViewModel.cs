using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UnleashedApp.Contracts;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Models;
using UnleashedApp.Repositories.HabitatRepositories;
using UnleashedApp.Repositories.SquadRepositories;
using UnleashedApp.Services;
using UnleashedApp.Views;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class WhoIsWhoViewModel : INotifyPropertyChanged, IWhoIsWhoViewModel
    {
        private readonly IHabitatRepository _habitatRepository;
        private readonly ISquadRepository _squadRepository;
        private readonly INavigationService _navigationService;

        private static ObservableCollection<Group> _filteredHabitatList;
        private static ObservableCollection<Group> _filteredSquadList;
        private static ObservableCollection<Group> _employeesPerHabitat;
        private static ObservableCollection<Group> _employeesPerSquad;
        private static Employee _selectedHabitatEmployee;
        private static Employee _selectedSquadEmployee;
        private static string _habitatFilter;
        private static string _squadFilter;

        public ICommand HabitatEmployeeDetailCommand { get; set; }
        public ICommand SquadEmployeeDetailCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public WhoIsWhoViewModel(INavigationService navigationService, IHabitatRepository habitatRepository,
            ISquadRepository squadRepository)
        {
            _navigationService = navigationService;
            _habitatRepository = habitatRepository;
            _squadRepository = squadRepository;
            InitialiseCommands();
            LoadEmployeesPerHabitat();
            RefreshFilteredHabitatList();
            LoadEmployeesPerSquad();
            RefreshFilteredSquadList();
        }

        #region LoadData

        private void LoadEmployeesPerHabitat()
        {
            List<Habitat> habitats = _habitatRepository.GetAllHabitats();
            if (habitats != null)
            {
                _employeesPerHabitat = new ObservableCollection<Group>();

                foreach (Habitat habitat in habitats)
                {
                    Group group = new Group(habitat);
                    List<Employee> employeesInHabitat = _habitatRepository.GetEmployees(group.Id);
                    if (employeesInHabitat != null && employeesInHabitat.Count > 0)
                    {
                        foreach (Employee employee in employeesInHabitat)
                        {
                            group.Add(employee);
                        }

                        _employeesPerHabitat.Add(group);
                    }
                }
            }
        }

        private void LoadEmployeesPerSquad()
        {
            List<Squad> squads = _squadRepository.GetAllSquads();
            if (squads != null)
            {
                _employeesPerSquad = new ObservableCollection<Group>();

                foreach (Squad squad in squads)
                {
                    Group group = new Group(squad);
                    List<Employee> employeesInSquad = _squadRepository.GetEmployees(group.Id);
                    if (employeesInSquad != null && employeesInSquad.Count > 0)
                    {
                        foreach (Employee employee in employeesInSquad)
                        {
                            group.Add(employee);
                        }

                        _employeesPerSquad.Add(group);
                    }
                }
            }
        }

        //Exposing for tests
        public ObservableCollection<Group> GetEmployeesPerHabitat()
        {
            return _employeesPerHabitat;
        }

        public ObservableCollection<Group> GetEmployeesPerSquad()
        {
            return _employeesPerSquad;
        }

        #endregion

        #region Properties 

        public ObservableCollection<Group> FilteredHabitatList
        {
            get => _filteredHabitatList;
            set
            {
                _filteredHabitatList = value;
                RaisePropertyChanged(nameof(FilteredHabitatList));
            }
        }

        public ObservableCollection<Group> FilteredSquadList
        {
            get => _filteredSquadList;
            set
            {
                _filteredSquadList = value;
                RaisePropertyChanged(nameof(FilteredSquadList));
            }
        }

        public Employee SelectedHabitatEmployee
        {
            get => _selectedHabitatEmployee;
            set
            {
                _selectedHabitatEmployee = value;
                RaisePropertyChanged(nameof(SelectedHabitatEmployee));
            }
        }

        public Employee SelectedSquadEmployee
        {
            get => _selectedSquadEmployee;
            set
            {
                _selectedSquadEmployee = value;
                RaisePropertyChanged(nameof(SelectedSquadEmployee));
            }
        }

        public string HabitatFilter
        {
            get => _habitatFilter;
            set
            {
                if (_habitatFilter != value)
                {
                    _habitatFilter = value;
                    RaisePropertyChanged("HabitatFilter");
                    FilterHabitatList();
                }
            }
        }

        public string SquadFilter
        {
            get => _squadFilter;
            set
            {
                if (_squadFilter != value)
                {
                    _squadFilter = value;
                    RaisePropertyChanged("SquadFilter");
                    FilterSquadList();
                }
            }
        }

        #endregion

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FilterHabitatList()
        {
            RefreshFilteredHabitatList();

            if (!String.IsNullOrEmpty(_habitatFilter))
            {
                List<Group> toBeRemovedGroups = new List<Group>();

                foreach (Group group in _filteredHabitatList)
                {
                    ObservableCollection<Employee> matchedEmployees = new ObservableCollection<Employee>();

                    foreach (Employee employee in group)
                    {
                        if (employee.FullName.ToLower().Contains(HabitatFilter.ToLower()))
                        {
                            matchedEmployees.Add(employee);
                        }
                    }

                    group.Clear();

                    foreach (Employee employee in matchedEmployees)
                    {
                        group.Add(employee);
                    }

                    if (group.Count == 0)
                    {
                        toBeRemovedGroups.Add(group);
                    }
                }

                foreach (Group group in toBeRemovedGroups)
                {
                    if (_filteredHabitatList.Contains(group))
                    {
                        _filteredHabitatList.Remove(group);
                    }
                }
            }
        }

        private void FilterSquadList()
        {
            RefreshFilteredSquadList();

            if (!String.IsNullOrEmpty(_squadFilter))
            {
                List<Group> toBeRemovedGroups = new List<Group>();

                foreach (Group group in _filteredSquadList)
                {
                    ObservableCollection<Employee> matchedEmployees = new ObservableCollection<Employee>();

                    foreach (Employee employee in group)
                    {
                        if (employee.FullName.ToLower().Contains(SquadFilter.ToLower()))
                        {
                            matchedEmployees.Add(employee);
                        }
                    }

                    group.Clear();

                    foreach (Employee employee in matchedEmployees)
                    {
                        group.Add(employee);
                    }

                    if (group.Count == 0)
                    {
                        toBeRemovedGroups.Add(group);
                    }
                }

                foreach (Group group in toBeRemovedGroups)
                {
                    if (_filteredSquadList.Contains(group))
                    {
                        _filteredSquadList.Remove(group);
                    }
                }
            }
        }

        #region Initialisation

        private void InitialiseCommands()
        {
            HabitatEmployeeDetailCommand = new Command(async () =>
            {
                TransferService.Store(SelectedHabitatEmployee);
                EmployeeDetailView employeeDetailPage = new EmployeeDetailView()
                {
                    BindingContext = SelectedHabitatEmployee
                };
                await App.NavigationPage.Navigation.PushAsync(employeeDetailPage);
                SelectedHabitatEmployee = null;
            });
            SquadEmployeeDetailCommand = new Command(async () =>
            {
                TransferService.Store(SelectedSquadEmployee);
                EmployeeDetailView employeeDetailPage = new EmployeeDetailView()
                {
                    BindingContext = SelectedSquadEmployee
                };
                await App.NavigationPage.Navigation.PushAsync(employeeDetailPage);
                SelectedSquadEmployee = null;
            });
        }

        #endregion

        private void RefreshFilteredHabitatList()
        {
            CopyObservableCollectionToFilteredHabitatList(_employeesPerHabitat);
        }

        private void RefreshFilteredSquadList()
        {
            CopyObservableCollectionToFilteredSquadList(_employeesPerSquad);
        }

        private void RefreshFilterHabitat()
        {
            string memory = HabitatFilter;
            HabitatFilter = "";
            HabitatFilter = memory;
        }

        private void RefreshFilterSquad()
        {
            string memory = SquadFilter;
            SquadFilter = "";
            SquadFilter = memory;
        }

        private void CopyObservableCollectionToFilteredHabitatList(ObservableCollection<Group> employeesPerGroup)
        {
            FilteredHabitatList = new ObservableCollection<Group>();
            foreach (Group group in employeesPerGroup)
            {
                Group g = new Group(group);
                foreach (Employee e in group)
                {
                    g.Add(new Employee(e.Id, e.FirstName, e.LastName, e.Function, e.HabitatId, e.StartDate, e.EndDate,
                        e.VisibleSite, e.PictureUrl,
                        e.Motivation, e.Expectations, e.NeedToKnow, e.DateOfBirth, e.Gender, e.Email));
                }

                FilteredHabitatList.Add(g);
            }
        }

        private void CopyObservableCollectionToFilteredSquadList(ObservableCollection<Group> employeesPerGroup)
        {
            FilteredSquadList = new ObservableCollection<Group>();
            foreach (Group group in employeesPerGroup)
            {
                Group g = new Group(group);
                foreach (Employee e in group)
                {
                    g.Add(new Employee(e.Id, e.FirstName, e.LastName, e.Function, e.HabitatId, e.StartDate, e.EndDate,
                        e.VisibleSite, e.PictureUrl,
                        e.Motivation, e.Expectations, e.NeedToKnow, e.DateOfBirth, e.Gender, e.Email));
                }

                FilteredSquadList.Add(g);
            }
        }
    }
}