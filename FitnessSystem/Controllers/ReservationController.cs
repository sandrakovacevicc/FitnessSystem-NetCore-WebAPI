using FitnessSystem.Application.DTOs.Client;
using FitnessSystem.Application.DTOs.Reservation;
using FitnessSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessSystem.Presentation.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReservationDto>>> GetAll(
            [FromQuery] string filterBy,
            [FromQuery] string filterValue,
            [FromQuery] string sortBy,
            [FromQuery] bool ascending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var reservations = await _reservationService.GetAllAsync(filterBy, filterValue, sortBy, ascending, pageNumber, pageSize);
            return Ok(reservations);
        }

        [HttpGet("clients/{clientJmbg}")]
        public async Task<ActionResult<List<ReservationDto>>> GetReservationsByClientJmbg(string clientJmbg)
        {
            var reservations = await _reservationService.GetReservationsByClientJmbgAsync(clientJmbg);
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reservationDto = await _reservationService.GetByIdAsync(id);
            if (reservationDto == null)
            {
                return NotFound();
            }
            return Ok(reservationDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationAddDto reservationAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdReservation = await _reservationService.CreateReservationAsync(reservationAddDto);
            return Ok(createdReservation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservationToDelete = await _reservationService.DeleteReservationAsync(id);
            if (reservationToDelete == null)
            {
                return NotFound(new { message = "Reservation not found." });
            }

            return Ok(new { message = "Reservation deleted successfully.", reservation = reservationToDelete });
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmReservationAsync([FromQuery] int sessionId, [FromQuery] string clientJmbg)
        {
            var reservationDto = await _reservationService.ConfirmReservationAsync(sessionId, clientJmbg);
            if (reservationDto == null)
            {
                return NotFound(new { message = "Reservation not found." });
            }

            return Ok(reservationDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReservationDto>> UpdateClient(int id, [FromBody] ReservationUpdateDto reservationUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedReservation = await _reservationService.UpdateReservationAsync(id, reservationUpdateDto);
            if (updatedReservation == null)
            {
                return NotFound(new { message = "Reservation not found." });
            }

            return Ok(updatedReservation);
        }

        [HttpGet("sessions/{sessionId}/confirmed-clients")]
        public async Task<ActionResult<List<ClientDto>>> GetConfirmedClientsBySessionId(int sessionId)
        {
            var confirmedClients = await _reservationService.GetConfirmedClientsBySessionIdAsync(sessionId);

            if (confirmedClients == null || confirmedClients.Count == 0)
            {
                return NotFound(new { message = "No confirmed clients found for the specified session." });
            }

            return Ok(confirmedClients);
        }


    }
}
