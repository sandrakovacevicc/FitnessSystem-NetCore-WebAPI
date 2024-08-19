using FitnessSystem.Application.DTOs.TrainingProgram;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessSystem.Presentation.Controllers
{
    [Route("api/training-programs")]
    [ApiController]
    public class TrainingProgramController : ControllerBase
    {
        private readonly ITrainingProgramService _trainingProgramService;

        public TrainingProgramController(ITrainingProgramService trainingProgramService)
        {
            _trainingProgramService = trainingProgramService;
        }

        [HttpGet]

        public async Task<ActionResult<List<TrainingProgramDto>>> GetAll()
        {
            var programs = await _trainingProgramService.GetAllAsync();
            return Ok(programs);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var programDto = await _trainingProgramService.GetByIdAsync(id);
            if (programDto == null)
            {
                return NotFound();
            }
            return Ok(programDto);
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchPrograms([FromQuery] string searchTerm)
        {
               var programs = await _trainingProgramService.SearchProgramsAsync(searchTerm);
               return Ok(programs);
        }
    }
}
