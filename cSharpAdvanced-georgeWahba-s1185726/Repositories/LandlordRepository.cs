using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using Microsoft.EntityFrameworkCore;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public class LandlordRepository : ILandlordRepository
    {
        private readonly cSharpAdvanced_georgeWahba_s1185726Context _context;

        public LandlordRepository(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Landlord>> GetAllLandlords()
        {
            return await _context.Landlord.ToListAsync();
        }

        public async Task<Landlord> GetLandlordById(int id)
        {
            return await _context.Landlord.FindAsync(id);
        }

        public async Task<Landlord> AddLandlord(Landlord landlord)
        {
            _context.Landlord.Add(landlord);
            await _context.SaveChangesAsync();
            return landlord;
        }

        public async Task<bool> UpdateLandlord(Landlord landlord)
        {
            _context.Entry(landlord).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LandlordExists(landlord.Id))
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

        public async Task<bool> DeleteLandlord(int id)
        {
            var landlord = await _context.Landlord.FindAsync(id);
            if (landlord == null)
            {
                return false;
            }

            _context.Landlord.Remove(landlord);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool LandlordExists(int id)
        {
            return _context.Landlord.Any(e => e.Id == id);
        }
    }
}
