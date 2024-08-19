using FitnessSystem.Application.DTOs.Reservation;
using FitnessSystem.Application.DTOs.Session;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessSystem.Presentation.Controllers
{
    [Route("api/sessions")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SessionDto>>> GetAll(
            [FromQuery] string filterBy,
            [FromQuery] string filterValue,
            [FromQuery] string sortBy,
            [FromQuery] bool ascending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var sessions = await _sessionService.GetAllAsync(filterBy, filterValue, sortBy, ascending, pageNumber, pageSize);
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sessionDto = await _sessionService.GetByIdAsync(id);
            if (sessionDto == null)
            {
                return NotFound();
            }
            return Ok(sessionDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] SessionAddDto sessionAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdSession = await _sessionService.CreateSessionAsync(sessionAddDto);
                return Ok(createdSession);
            }
            catch (InvalidOperationException)
            {
                return Conflict("Trainer is already busy in that time.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the session.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var sessionToDelete = await _sessionService.DeleteSessionAsync(id);
            if (sessionToDelete == null)
            {
                return NotFound(new { message = "Session not found." });
            }

            return Ok(new { message = "Session deleted successfully.", reservation = sessionToDelete });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SessionDto>> UpdateSession(int id, [FromBody] SessionUpdateDto sessionUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedSession = await _sessionService.UpdateSessionAsync(id, sessionUpdateDto);
                return Ok(updatedSession);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("trainers/{trainerJMBG}")]
        public async Task<ActionResult<List<ReservationDto>>> GetSessionsByTrainerJmbg(string trainerJMBG)
        {
            var sessions = await _sessionService.GetSessionsByTrainerJmbgAsync(trainerJMBG);
            return Ok(sessions);
        }

        [HttpGet("{id}/qrcode")]
        public async Task<IActionResult> GenerateQrCode(int id)
        {
            try
            {
                var qrCodeImage = await _sessionService.GenerateQrCodeAsync(id);

                if (qrCodeImage == null)
                {
                    return NotFound("Session not found.");
                }

                return File(qrCodeImage, "image/png");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Session not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
