using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private IHabitatRepository _habitatRepository;
        private ISquadRepository _squadRepository;
        private readonly INavigationService _navigationService;
        private ObservableCollection<Employee> _employees;
        private ObservableCollection<Group> _groupedList;
        private ObservableCollection<Group> _filteredList;
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
            LoadHabitats();
        }

        #region LoadData
        public void LoadHabitats()
        {
            var habitats = _habitatRepository.GetAllHabitats();

            if (habitats != null)
            {
                GroupedList = new ObservableCollection<Group>();

                foreach (Habitat habitat in habitats)
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

                    if (employees != null && employees.Count > 0)

                        foreach (Employee employee in employees)
                        {
                            {
                                group.Add(employee);
                            }
                        }
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
                    group.Add(employee);
                }
            }

            InitialiseFilteredList();
        }
        #endregion

        #region Properties
        public ObservableCollection<Group> GroupedList
        {
            get => _groupedList;
            set
            {
                _groupedList = value;
                RaisePropertyChanged(nameof(GroupedList));
            }
        }

        public ObservableCollection<Group> FilteredList
        {
            get => _filteredList;
            set
            {
                _filteredList = value;
                RaisePropertyChanged(nameof(FilteredList));
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

        private string _filter;
        public string Filter
        {
            get
            {
                return _filter;
            }
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
            if (_groupedList != null)
            {
                if (String.IsNullOrEmpty(_filter))
                {
                    InitialiseFilteredList();
                }
                else
                {
                    InitialiseFilteredList();

                    foreach (Group group in FilteredList)
                    {
                        ObservableCollection<Employee> temp = new ObservableCollection<Employee>();

                        foreach (Employee emp in group)
                        {
                            if (emp.FullName.ToLower().Contains(Filter.ToLower()))
                            {
                                temp.Add(emp);
                            }
                        }

                        group.Clear();
                        foreach (Employee emp in temp)
                        {
                            group.Add(emp);
                        }
                    }
                }
            }
        }

        #region Initialisation
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

        private void InitialiseFilteredList()
        {
            FilteredList = new ObservableCollection<Group>();

            foreach (Group group in GroupedList)
            {
                var gr = new Group();
                gr.Id = group.Id;
                gr.Name = group.Name;

                foreach (Employee employee in group)
                {
                    var emp = new Employee();
                    emp.Id = employee.Id;
                    emp.FirstName = employee.FirstName;
                    emp.LastName = employee.LastName;
                    emp.StartDate = employee.StartDate;
                    emp.EndDate = employee.EndDate;
                    emp.Function = employee.Function;
                    emp.Habitat_Id = employee.Habitat_Id;

                    gr.Add(emp);
                }

                FilteredList.Add(gr);
            }
        }
        #endregion
    }
}
