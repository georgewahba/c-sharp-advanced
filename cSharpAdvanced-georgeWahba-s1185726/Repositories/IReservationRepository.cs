using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Models;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllReservations(CancellationToken cancellationToken);
        Task<Reservation> GetReservationById(int id, CancellationToken cancellationToken);
        Task<Reservation> AddReservation(Reservation reservation, CancellationToken cancellationToken);
        Task<bool> UpdateReservation(Reservation reservation, CancellationToken cancellationToken);
        Task<bool> DeleteReservation(int id, CancellationToken cancellationToken);
    }
}
 