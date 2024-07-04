using FitnessSystem.Application.DTOs;
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

        [HttpPost]
        public async Task<IActionResult> CreateProgram([FromBody] TrainingProgramDto trainingProgramDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdProgram = await _trainingProgramService.CreateTrainingProgramAsync(trainingProgramDto);
                return Ok(createdProgram);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while creating the admin.");
            }
        }
    }
}
