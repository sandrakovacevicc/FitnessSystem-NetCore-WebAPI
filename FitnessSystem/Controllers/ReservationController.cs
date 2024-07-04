using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<ReservationDto>>> GetAll()
        {
            var reservations = await _reservationService.GetAllAsync();
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

            try
            {
                var createdReservation = await _reservationService.CreateReservationAsync(reservationAddDto);
                return Ok(createdReservation);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while creating the admin.");
            }
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
    }
}
