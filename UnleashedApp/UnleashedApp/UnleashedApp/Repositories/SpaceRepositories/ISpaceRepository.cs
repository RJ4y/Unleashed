using System.Collections.Generic;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.SpaceRepositories
{
    public interface ISpaceRepository
    {
        List<Space> GetAllSpaces();
    }
}
