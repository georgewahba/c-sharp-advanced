using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using cSharpAdvanced_georgeWahba_s1185726.Repositories;
using AutoMapper;

namespace cSharpAdvanced_georgeWahba_s1185726.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
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
        public async Task<ActionResult<ReservationResponseDTO>> PostReservation(ReservationRequestDTO request, CancellationToken cancellationToken)
        {
            // Zoek de klant op basis van het e-mailadres of maak een nieuwe klant aan
            var customer = await _reservationRepository.GetOrCreateCustomerByEmail(request.Email, request.FirstName, request.LastName, cancellationToken);

            // Maak een nieuwe reservering aan
            var reservation = new Reservation
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                LocationId = request.LocationId,
                CustomerId = customer.Id,
                Discount = request.Discount ?? 0 
            };

            // Sla de reservering op in de database
            var createdReservation = await _reservationRepository.AddReservation(reservation, cancellationToken);

            // Bereken de prijs van de reservering
            var price = await _reservationRepository.CalculateReservationPrice(createdReservation, cancellationToken);

            // Bouw het antwoord DTO op
            var responseDTO = new ReservationResponseDTO
            {
                LocationName = createdReservation.Location.Title,
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                Price = price,
                Discount = createdReservation.Discount
            };

            return Ok(responseDTO);
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
