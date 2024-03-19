using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cSharpAdvanced_georgeWahba_s1185726.DTOs;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using cSharpAdvanced_georgeWahba_s1185726.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using cSharpAdvanced_georgeWahba_s1185726.Data;

namespace cSharpAdvanced_georgeWahba_s1185726.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        private readonly cSharpAdvanced_georgeWahba_s1185726Context _context;

        public LocationsController(ILocationRepository locationRepository, IMapper mapper, cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
            _context = context; 
        }

        // GET: api/Locations/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocation(CancellationToken cancellationToken)
        {
            var locations = await _locationRepository.GetAllLocations(cancellationToken);
            if (locations == null || !locations.Any())
            {
                return NotFound();
            }

            return Ok(locations);
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetAllLocations(CancellationToken cancellationToken)
        {
            var locations = await _locationRepository.GetAllLocations(cancellationToken);
            if (locations == null || !locations.Any())
            {
                return NotFound();
            }

            var mappedLocations = _mapper.Map<IEnumerable<LocationDTO>>(locations);
            return Ok(mappedLocations);
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDTO>> GetLocation(int id, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetLocationById(id, cancellationToken);

            if (location == null)
            {
                return NotFound();
            }

            var mappedLocation = _mapper.Map<LocationDTO>(location);
            return mappedLocation;
        }

        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, LocationDTO locationDTO, CancellationToken cancellationToken)
        {
            var existingLocation = await _locationRepository.GetLocationById(id, cancellationToken);
            if (existingLocation == null)
            {
                return NotFound();
            }

            var location = _mapper.Map<Location>(locationDTO);
            location.Id = id;

            var success = await _locationRepository.UpdateLocation(location, cancellationToken);
            if (!success)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Locations
        [HttpPost]
        public async Task<ActionResult<LocationDTO>> PostLocation(LocationDTO locationDTO, CancellationToken cancellationToken)
        {
            var location = _mapper.Map<Location>(locationDTO);

            var createdLocation = await _locationRepository.AddLocation(location, cancellationToken);

            var mappedLocation = _mapper.Map<LocationDTO>(createdLocation);

            return CreatedAtAction(nameof(GetLocation), new { id = mappedLocation.Id }, mappedLocation);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id, CancellationToken cancellationToken)
        {
            var existingLocation = await _locationRepository.GetLocationById(id, cancellationToken);
            if (existingLocation == null)
            {
                return NotFound();
            }

            var success = await _locationRepository.DeleteLocation(id, cancellationToken);
            if (!success)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Locations/Search
        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<Location2DTO>>> Search([FromBody] SearchRequestDTO request, CancellationToken cancellationToken)
        {
            var locations = await _locationRepository.SearchLocations(request, cancellationToken);

            if (locations == null || !locations.Any())
            {
                return NotFound();
            }

            var mappedLocations = _mapper.Map<IEnumerable<Location2DTO>>(locations);
            return Ok(mappedLocations);
        }

        // GET: api/Locations/GetMaxPrice
        [HttpGet("GetMaxPrice")]
        public async Task<ActionResult<MaxPriceDTO>> GetMaxPrice()
        {
            // Query the database to find the maximum price of locations
            var maxPrice = await _context.Location.MaxAsync(location => (int?)location.PricePerDay) ?? 0;

            var maxPriceDTO = new MaxPriceDTO { Price = maxPrice };

            return Ok(maxPriceDTO);
        }

        // GET: api/Locations/GetDetails/{id}
        [HttpGet("GetDetails/{id}")]
        public async Task<ActionResult<LocationDetailsDTO>> GetDetails(int id, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetLocationById(id, cancellationToken);

            if (location == null)
            {
                return NotFound();
            }

            var locationDetailsDTO = _mapper.Map<LocationDetailsDTO>(location);
            return locationDetailsDTO;
        }
    }
}
