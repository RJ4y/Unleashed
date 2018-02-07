using System.Collections.Generic;
using UnleashedApp.Models;
using UnleashedApp.Repositories.EmployeeRepositories;

namespace UnleashedApp.ViewModels
{
    public class NameGameViewModel
    {
        public List<Employee> Employees { get; }

        public NameGameViewModel(IEmployeeRepository employeeRepository)
        {
            InitialiseCommands();
            Employees = employeeRepository.GetAllEmployees();
        }

        private void InitialiseCommands()
        {
        }
    }
}