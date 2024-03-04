using System.Collections.Generic;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Models;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocations();
        Task<Location> GetLocationById(int id);
        Task<Location> AddLocation(Location location);
        Task<bool> UpdateLocation(Location location);
        Task<bool> DeleteLocation(int id);
    }
}

