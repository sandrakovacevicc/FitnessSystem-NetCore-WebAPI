using FitnessSystem.Application.DTOs;
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
        public async Task<ActionResult<List<SessionDto>>> GetAll()
        {
            var sessions = await _sessionService.GetAllAsync();
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
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while creating the admin.");
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
    }
}
