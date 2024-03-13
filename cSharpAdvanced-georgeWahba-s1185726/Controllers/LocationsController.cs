using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.DTOs;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using cSharpAdvanced_georgeWahba_s1185726.Repositories;

namespace cSharpAdvanced_georgeWahba_s1185726.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private ILocationRepository locationRepository;
        private readonly cSharpAdvanced_georgeWahba_s1185726Context _context;
        private readonly IMapper _mapper;

        public LocationsController(cSharpAdvanced_georgeWahba_s1185726Context context, IMapper mapper)
        {
            locationRepository = new LocationRepository(context);
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Locations/GetAll
        [HttpGet("GetAll")]

        public async Task<ActionResult<IEnumerable<Location>>> GetLocation()
        {
            if (_context.Location == null)
            {
                return NotFound();
            }
            return await _context.Location.ToListAsync();
        }
        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetAllLocations()
        {
            var locations = await locationRepository.GetAllLocations();
            if (locations == null || !locations.Any())
            {
                return NotFound();
            }

            var mappedLocations = _mapper.Map<IEnumerable<LocationDTO>>(locations);
            return Ok(_mapper.Map<List<LocationDTO>>(locations));
        }


        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDTO>> GetLocation(int id)
        {
            var location = await _context.Location.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            var mappedLocation = _mapper.Map<LocationDTO>(location);
            return mappedLocation;
        }

        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, LocationDTO locationDTO)
        {
            if (id != locationDTO.Id)
            {
                return BadRequest();
            }

            var location = _mapper.Map<Location>(locationDTO);

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Locations
        [HttpPost]
        public async Task<ActionResult<LocationDTO>> PostLocation(LocationDTO locationDTO)
        {
            var location = _mapper.Map<Location>(locationDTO);

            _context.Location.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = location.Id }, locationDTO);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Location.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.Id == id);
        }
    }
}
