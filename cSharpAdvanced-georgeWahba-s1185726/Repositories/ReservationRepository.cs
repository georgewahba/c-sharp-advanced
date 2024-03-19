﻿using System.Linq;
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

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
