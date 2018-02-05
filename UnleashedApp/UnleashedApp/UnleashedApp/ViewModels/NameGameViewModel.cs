using System.Collections.Generic;
using UnleashedApp.Models;
using UnleashedApp.Repositories.EmployeeRepositories;

namespace UnleashedApp.ViewModels
{
    public class NameGameViewModel
    {
        private readonly INavigationService _navigationService;
        private IEmployeeRepository _employeeRepository;
        public List<Employee> Employees { get; set; }

        public NameGameViewModel(INavigationService navigationService, IEmployeeRepository employeeRepository)
        {
            _navigationService = navigationService;
            _employeeRepository = employeeRepository;
            InitialiseCommands();
            Employees = _employeeRepository.GetAllEmployees();
        }

        private void InitialiseCommands()
        {
        }
    }
}
