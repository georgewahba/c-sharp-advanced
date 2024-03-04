using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using Microsoft.EntityFrameworkCore;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly cSharpAdvanced_georgeWahba_s1185726Context _context;

        public LocationRepository(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            return await _context.Location.ToListAsync();
        }

        public async Task<Location> GetLocationById(int id)
        {
            return await _context.Location.FindAsync(id);
        }

        public async Task<Location> AddLocation(Location location)
        {
            _context.Location.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<bool> UpdateLocation(Location location)
        {
            _context.Entry(location).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
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

        public async Task<bool> DeleteLocation(int id)
        {
            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return false;
            }

            _context.Location.Remove(location);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.Id == id);
        }
    }
}
