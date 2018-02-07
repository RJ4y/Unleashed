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
        #region Variables
        private readonly IHabitatRepository _habitatRepository;
        private readonly ISquadRepository _squadRepository;
        private readonly INavigationService _navigationService;

        private static ObservableCollection<Group> _filteredList;
        private static ObservableCollection<Group> _employeesPerHabitat;
        private static ObservableCollection<Group> _employeesPerSquad;
        private static Employee _selectedEmployee;
        private static string _filter;

        private static bool isSortingHabitats = true;
        private static bool shouldLoadHabitatData = true;
        private static bool shouldLoadSquadData = true;

        public ICommand EmployeeDetailCommand { get; set; }
        public ICommand HabitatCommand { get; set; }
        public ICommand SquadCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public WhoIsWhoViewModel(INavigationService navigationService, IHabitatRepository habitatRepository, ISquadRepository squadRepository)
        {
            _navigationService = navigationService;
            _habitatRepository = habitatRepository;
            _squadRepository = squadRepository;
            InitialiseCommands();
            LoadEmployeesPerHabitat();
            RefreshFilteredList();
        }

        #region LoadData
        public void LoadEmployeesPerHabitat()
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
            shouldLoadHabitatData = false;
        }

        public void LoadEmployeesPerSquad()
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
            shouldLoadSquadData = false;
        }
        #endregion

        #region Properties 
        public ObservableCollection<Group> FilteredList
        {
            get => _filteredList;
            set
            {
                _filteredList = value;
                RaisePropertyChanged(nameof(FilteredList));
            }
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                RaisePropertyChanged(nameof(SelectedEmployee));
            }
        }

        public string Filter
        {
            get => _filter;
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    RaisePropertyChanged("Filter");
                    FilterList();
                }
            }
        }
        #endregion

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FilterList()
        {
            RefreshFilteredList();

            if (!String.IsNullOrEmpty(_filter))
            {
                RefreshFilteredList();
                List<Group> emptyGroups = new List<Group>();
                List<Group> toBeRemovedGroups = new List<Group>();
                foreach (Group group in _filteredList)
                {
                    ObservableCollection<Employee> matchedEmployees = new ObservableCollection<Employee>();

                    foreach (Employee employee in group)
                    {
                        if (employee.FullName.ToLower().Contains(Filter.ToLower()))
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
                    if (_filteredList.Contains(group))
                    {
                        _filteredList.Remove(group);
                    }
                }
            }
        }

        #region Initialisation
        private void InitialiseCommands()
        {
            EmployeeDetailCommand = new Command(async () =>
            {
                EmployeeDetailView empDetailPage = new EmployeeDetailView();
                empDetailPage.BindingContext = SelectedEmployee;

                await _navigationService.PushAsync(empDetailPage);
                SelectedEmployee = null;
            });
            HabitatCommand = new Command(() =>
            {
                if (shouldLoadHabitatData)
                {
                    LoadEmployeesPerHabitat();
                }
                else
                {
                    isSortingHabitats = true;
                }
                RefreshFilteredList();
                RefreshFilter();
            });
            SquadCommand = new Command(() =>
            {
                if (shouldLoadSquadData)
                {
                    LoadEmployeesPerSquad();
                }
                isSortingHabitats = false;
                RefreshFilteredList();
                RefreshFilter();
            });
        }
        #endregion

        private void RefreshFilteredList()
        {
            if (isSortingHabitats)
            {
                CopyObservableCollectionToFilteredList(_employeesPerHabitat);
            }
            else
            {
                CopyObservableCollectionToFilteredList(_employeesPerSquad);
            }
        }

        private void RefreshFilter()
        {
            string memory = Filter;
            Filter = "";
            Filter = memory;
        }

        private void CopyObservableCollectionToFilteredList(ObservableCollection<Group> employeesPerGroup)
        {
            FilteredList = new ObservableCollection<Group>();
            foreach (Group group in employeesPerGroup)
            {
                Group g = new Group(group);
                foreach (Employee e in group)
                {
                    g.Add(new Employee(e.Id, e.FirstName, e.LastName, e.StartDate, e.EndDate, e.Function, e.Habitat_Id));
                }
                FilteredList.Add(g);
            }
        }
    }
}