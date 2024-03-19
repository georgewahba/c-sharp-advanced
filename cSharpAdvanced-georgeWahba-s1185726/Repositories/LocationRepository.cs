using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.DTOs;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using Microsoft.EntityFrameworkCore;
using static cSharpAdvanced_georgeWahba_s1185726.Models.Location;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly cSharpAdvanced_georgeWahba_s1185726Context _context;

        public LocationRepository(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllLocations(CancellationToken cancellationToken)
        {
            return await _context.Location
                .Include(location => location.Landlord)
                .Include(location => location.Images)
                .ToListAsync(cancellationToken);
        }

        public async Task<Location> GetLocationById(int id, CancellationToken cancellationToken)
        {
            return await _context.Location.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<Location> AddLocation(Location location, CancellationToken cancellationToken)
        {
            _context.Location.Add(location);
            await _context.SaveChangesAsync(cancellationToken);
            return location;
        }

        public async Task<bool> UpdateLocation(Location location, CancellationToken cancellationToken)
        {
            _context.Entry(location).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(location.Id))
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

        public async Task<bool> DeleteLocation(int id, CancellationToken cancellationToken)
        {
            var location = await _context.Location.FindAsync(new object[] { id }, cancellationToken);
            if (location == null)
            {
                return false;
            }

            _context.Location.Remove(location);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<Location>> SearchLocations(SearchRequestDTO request, CancellationToken cancellationToken)
        {
            var query = _context.Location.AsQueryable();

            if (request.Features.HasValue)
            {
                query = query.Where(location => location.Feature.HasFlag((Location.Features)request.Features.Value));
            }

            if (request.Type.HasValue)
            {
                query = query.Where(location => location.Type == (LocationType)request.Type.Value);
            }

            if (request.Rooms.HasValue)
            {
                query = query.Where(location => location.Rooms >= request.Rooms.Value);
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(location => location.PricePerDay >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(location => location.PricePerDay <= request.MaxPrice.Value);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
