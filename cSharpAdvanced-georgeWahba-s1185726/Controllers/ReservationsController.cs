using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using cSharpAdvanced_georgeWahba_s1185726.Repositories;

namespace cSharpAdvanced_georgeWahba_s1185726.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationsController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservation(CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetAllReservations(cancellationToken);
            return Ok(reservations);
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetReservationById(id, cancellationToken);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation, CancellationToken cancellationToken)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            var updated = await _reservationRepository.UpdateReservation(reservation, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Reservations
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation, CancellationToken cancellationToken)
        {
            var createdReservation = await _reservationRepository.AddReservation(reservation, cancellationToken);
            return CreatedAtAction("GetReservation", new { id = createdReservation.Id }, createdReservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id, CancellationToken cancellationToken)
        {
            var deleted = await _reservationRepository.DeleteReservation(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
