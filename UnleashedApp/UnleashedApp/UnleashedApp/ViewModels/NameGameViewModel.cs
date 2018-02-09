using System.Collections.Generic;
using UnleashedApp.Models;
using UnleashedApp.Repositories.EmployeeRepositories;

namespace UnleashedApp.ViewModels
{
    public class NameGameViewModel
    {
        public List<Employee> Employees { get; set; }
        public IEmployeeRepository employeeRepository;

        public NameGameViewModel(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public void LoadEmployees()
        {
            Employees = employeeRepository.GetAllEmployees();
        }
    }
}