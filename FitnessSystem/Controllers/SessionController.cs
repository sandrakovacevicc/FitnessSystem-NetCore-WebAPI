using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
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
    }
}
