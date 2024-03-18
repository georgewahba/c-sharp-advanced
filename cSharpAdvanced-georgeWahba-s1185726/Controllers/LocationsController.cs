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
    /// <summary>
    /// Controller for managing locations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly cSharpAdvanced_georgeWahba_s1185726Context _context;
        private readonly IMapper _mapper;

        public LocationsController(cSharpAdvanced_georgeWahba_s1185726Context context, IMapper mapper, ILocationRepository locationRepository)
        {
            _context = context;
            _mapper = mapper;
            _locationRepository = locationRepository;
        }

        /// <summary>
        /// Gets all locations.
        /// </summary>
        /// <returns>A list of locations.</returns>
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations()
        {
            var locations = await _locationRepository.GetAllLocations();
            if (locations == null || !locations.Any())
            {
                return NotFound();
            }

            return Ok(locations);
        }

        /// <summary>
        /// Gets a location by ID.
        /// </summary>
        /// <param name="id">The ID of the location to retrieve.</param>
        /// <returns>The location with the specified ID.</returns>
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

        /// <summary>
        /// Updates a location.
        /// </summary>
        /// <param name="id">The ID of the location to update.</param>
        /// <param name="locationDTO">The updated location data.</param>
        /// <returns>No content if successful.</returns>
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

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="locationDTO">The location data to create.</param>
        /// <returns>The created location.</returns>
        [HttpPost]
        public async Task<ActionResult<LocationDTO>> PostLocation(LocationDTO locationDTO)
        {
            var location = _mapper.Map<Location>(locationDTO);

            _context.Location.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, locationDTO);
        }

        /// <summary>
        /// Deletes a location.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <returns>No content if successful.</returns>
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
