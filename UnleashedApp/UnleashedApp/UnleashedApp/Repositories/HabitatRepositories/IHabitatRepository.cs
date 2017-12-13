using System.Collections.Generic;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.HabitatRepositories
{
    public interface IHabitatRepository
    {
        List<Habitat> GetAllHabitats();
    }
}
