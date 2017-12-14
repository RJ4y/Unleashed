using System.Collections.Generic;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.EmployeeRepositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
    }
}
