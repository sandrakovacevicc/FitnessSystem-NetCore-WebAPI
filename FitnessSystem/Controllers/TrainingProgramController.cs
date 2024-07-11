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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingProgram(int id)
        {
            var trainingProgramToDelete = await _trainingProgramService.DeleteTrainingProgramAsync(id);
            if (trainingProgramToDelete == null)
            {
                return NotFound(new { message = "TrainingProgram not found." });
            }

            return Ok(new { message = "TrainingProgram deleted successfully.", room = trainingProgramToDelete });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TrainingProgramDeleteDto>> UpdateClient(int id, [FromBody] TrainingProgramDto trainingProgramDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedRoom = await _trainingProgramService.UpdateTrainingProgramAsync(id, trainingProgramDto);
                return Ok(updatedRoom);
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
    }
}
