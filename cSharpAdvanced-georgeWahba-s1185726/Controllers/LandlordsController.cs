using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using cSharpAdvanced_georgeWahba_s1185726.Repositories; // Add this namespace
using cSharpAdvanced_georgeWahba_s1185726.Services;

namespace cSharpAdvanced_georgeWahba_s1185726.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandlordsController : ControllerBase
    {
        private readonly ILandlordRepository _landlordRepository;
        private readonly cSharpAdvanced_georgeWahba_s1185726Context _context; // Inject the context
        private readonly SearchService _searchService;

        public LandlordsController(ILandlordRepository landlordRepository, cSharpAdvanced_georgeWahba_s1185726Context context, SearchService searchService)
        {
            _landlordRepository = landlordRepository;
            _context = context;
            _searchService = searchService;
        }

        // GET: api/Landlords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Landlord>>> GetLandlord()
        {
            var landlords = await _landlordRepository.GetAllLandlords();
            return Ok(landlords);
        }

        [HttpGet("locations")]
        public ActionResult<IEnumerable<Location>> GetAllLocations()
        {
            var locations = _searchService.GetAllLocations(_context); // Pass the context to the method
            if (locations == null || !locations.Any())
            {
                return NotFound("No locations found.");
            }

            return Ok(locations);
        }

        // GET: api/Landlords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Landlord>> GetLandlord(int id)
        {
            var landlord = await _landlordRepository.GetLandlordById(id);
            if (landlord == null)
            {
                return NotFound();
            }
            return Ok(landlord);
        }

        // PUT: api/Landlords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLandlord(int id, Landlord landlord)
        {
            if (id != landlord.Id)
            {
                return BadRequest();
            }

            var updated = await _landlordRepository.UpdateLandlord(landlord);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Landlords
        [HttpPost]
        public async Task<ActionResult<Landlord>> PostLandlord(Landlord landlord)
        {
            var createdLandlord = await _landlordRepository.AddLandlord(landlord);
            return CreatedAtAction("GetLandlord", new { id = createdLandlord.Id }, createdLandlord);
        }

        // DELETE: api/Landlords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLandlord(int id)
        {
            var deleted = await _landlordRepository.DeleteLandlord(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
