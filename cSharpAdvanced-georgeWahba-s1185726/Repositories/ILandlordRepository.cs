using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Models;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public interface ILandlordRepository
    {
        Task<IEnumerable<Landlord>> GetAllLandlords(CancellationToken cancellationToken);
        Task<Landlord> GetLandlordById(int id);
        Task<Landlord> AddLandlord(Landlord landlord);
        Task<bool> UpdateLandlord(Landlord landlord);
        Task<bool> DeleteLandlord(int id);
    }
}
