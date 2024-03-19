using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.DTOs;
using cSharpAdvanced_georgeWahba_s1185726.Models;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocations(CancellationToken cancellationToken);
        Task<Location> GetLocationById(int id, CancellationToken cancellationToken);
        Task<Location> AddLocation(Location location, CancellationToken cancellationToken);
        Task<bool> UpdateLocation(Location location, CancellationToken cancellationToken);
        Task<bool> DeleteLocation(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Location>> SearchLocations(SearchRequestDTO request, CancellationToken cancellationToken);
    }
}
