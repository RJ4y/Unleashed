using System.Collections.Generic;
using UnleashedApp.Models;
using UnleashedApp.Repositories.EmployeeRepositories;

namespace UnleashedApp.ViewModels
{
    public class NameGameViewModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        public List<Employee> Employees { get; private set; }

        public NameGameViewModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            InitialiseCommands();
            Employees = _employeeRepository.GetAllEmployees();
        }

        private void InitialiseCommands()
        {
        }
    }
}
