using System.Collections.Generic;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.SquadRepositories
{
    public interface ISquadRepository
    {
        List<Squad> GetAllSquads();
        Squad GetSquadById(int id);
        List<Employee> GetEmployees(int id);
    }
}