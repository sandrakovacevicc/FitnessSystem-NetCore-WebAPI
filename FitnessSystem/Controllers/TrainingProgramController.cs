using FitnessSystem.Application.DTOs.TrainingProgram;
using FitnessSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<List<TrainingProgramDto>>> GetAllTrainingPrograms()
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
                return NotFound(new { message = "Training program not found." });
            }
            return Ok(programDto);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPrograms([FromQuery] string searchTerm)
        {
            var programs = await _trainingProgramService.SearchProgramsAsync(searchTerm);
            return Ok(programs);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TrainingProgramDto>> UpdateTrainingProgram(int id, [FromBody] TrainingProgramDto trainingProgramDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTrainingProgram = await _trainingProgramService.UpdateTrainingProgramAsync(id, trainingProgramDto);
            if (updatedTrainingProgram == null)
            {
                return NotFound(new { message = "Training program not found." });
            }

            return Ok(updatedTrainingProgram);
        }
    }
}
