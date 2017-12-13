using System.Collections.Generic;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.SquadRepositories
{
    public interface ISquadRepository
    {
        List<Squad> GetAllSquads();
    }
}
