using System.Collections.Generic;
using System.Linq;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using cSharpAdvanced_georgeWahba_s1185726.Data;

namespace cSharpAdvanced_georgeWahba_s1185726.Services
{
    public class SearchService
    {
        private readonly cSharpAdvanced_georgeWahba_s1185726Context _context;

        public SearchService(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            _context = context;
        }

        public IEnumerable<Location> GetAllLocations(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            return context.Location.ToList();
        }

        public IEnumerable<Location> SearchLocations(string searchTerm)
        {
            var result = _context.Location
                                .Where(l => l.Title.Contains(searchTerm) || l.SubTitle.Contains(searchTerm))
                                .ToList();
            return result;
        }

    }
}
