using System.Collections.Generic;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Models;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllReservations();
        Task<Reservation> GetReservationById(int id);
        Task<Reservation> AddReservation(Reservation reservation);
        Task<bool> UpdateReservation(Reservation reservation);
        Task<bool> DeleteReservation(int id);
    }
}
