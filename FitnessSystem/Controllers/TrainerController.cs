using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessSystem.Presentation.Controllers
{
    [Route("api/trainers")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TrainerDto>>> GetAll()
        {
            var trainers = await _trainerService.GetAllAsync();
            return Ok(trainers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trainerDto = await _trainerService.GetByIdAsync(id);
            if (trainerDto == null)
            {
                return NotFound();
            }
            return Ok(trainerDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrainer([FromBody] TrainerAddDto trainerAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdTrainer = await _trainerService.CreateTrainerAsync(trainerAddDto);
                return Ok(createdTrainer);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while creating the admin.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var trainerToDelete = await _trainerService.DeleteTrainerAsync(id);
            if (trainerToDelete == null)
            {
                return NotFound(new { message = "Trainer not found." });
            }

            return Ok(new { message = "Trainer deleted successfully.", room = trainerToDelete });
        }
    }
}
