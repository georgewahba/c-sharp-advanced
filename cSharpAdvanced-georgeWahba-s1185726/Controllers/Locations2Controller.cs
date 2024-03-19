using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.DTOs;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using cSharpAdvanced_georgeWahba_s1185726.Repositories;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using System.Threading;     

namespace cSharpAdvanced_georgeWahba_s1185726.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/Locations")]
    [ApiController]
    public class Locations2Controller : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public Locations2Controller(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location2DTO>>> GetAllLocations()
        {
            var locations = await _locationRepository.GetAllLocations(CancellationToken.None);
            if (locations == null || !locations.Any())
            {
                return NotFound();
            }

            var location2Dtos = _mapper.Map<IEnumerable<Location2DTO>>(locations);
            return Ok(location2Dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location2DTO>> GetLocation(int id)
        {
            var location = await _locationRepository.GetLocationById(id, CancellationToken.None); // Pass CancellationToken.None

            if (location == null)
            {
                return NotFound();
            }

            var locationDto = _mapper.Map<Location2DTO>(location);
            return Ok(locationDto);
        }

        [HttpPost]
        public async Task<ActionResult<Location2DTO>> CreateLocation(Location2DTO locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            await _locationRepository.AddLocation(location, CancellationToken.None); // Pass CancellationToken.None
            return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, locationDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var success = await _locationRepository.DeleteLocation(id, CancellationToken.None); // Pass CancellationToken.None
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
