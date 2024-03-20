using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using Microsoft.EntityFrameworkCore;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly cSharpAdvanced_georgeWahba_s1185726Context _context;

        public ReservationRepository(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations(CancellationToken cancellationToken)
        {
            return await _context.Reservation.ToListAsync(cancellationToken);
        }

        public async Task<Reservation> GetReservationById(int id, CancellationToken cancellationToken)
        {
            return await _context.Reservation.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<Reservation> AddReservation(Reservation reservation, CancellationToken cancellationToken)
        {
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync(cancellationToken);
            return reservation;
        }

        public async Task<bool> UpdateReservation(Reservation reservation, CancellationToken cancellationToken)
        {
            _context.Entry(reservation).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(reservation.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public async Task<bool> DeleteReservation(int id, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservation.FindAsync(new object[] { id }, cancellationToken);
            if (reservation == null)
            {
                return false;
            }

            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Customer> GetOrCreateCustomerByEmail(string email, string firstName, string lastName, CancellationToken cancellationToken)
        {
            var existingCustomer = await _context.Customer.FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
            if (existingCustomer != null)
            {
                return existingCustomer;
            }
            else
            {
                var newCustomer = new Customer { Email = email, FirstName = firstName, LastName = lastName }; // Voeg voornaam en achternaam toe
                _context.Customer.Add(newCustomer);
                await _context.SaveChangesAsync(cancellationToken);
                return newCustomer;
            }
        }

        public async Task<float> CalculateReservationPrice(Reservation reservation, CancellationToken cancellationToken)
        {
            var location = await _context.Location.FindAsync(reservation.LocationId);
            if (location == null)
            {
                throw new InvalidOperationException("Invalid location ID");
            }

            // Calculate the number of days between the start and end dates
            var numberOfDays = (reservation.EndDate - reservation.StartDate).Days;

            // Calculate the total price based on the price per day of the location and the number of days
            var totalPrice = location.PricePerDay * numberOfDays;

            // Apply discount if available
            if (reservation.Discount > 0 && reservation.Discount <= 1)
            {
                totalPrice -= totalPrice * reservation.Discount;
            }

            return totalPrice;
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
