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

        [HttpGet("{jmbg}")]
        public async Task<IActionResult> GetById(string jmbg)
        {
            var trainerDto = await _trainerService.GetByIdAsync(jmbg);
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

        [HttpDelete("{jmbg}")]
        public async Task<IActionResult> DeleteTrainer(string jmbg)
        {
            var trainerToDelete = await _trainerService.DeleteTrainerAsync(jmbg);
            if (trainerToDelete == null)
            {
                return NotFound(new { message = "Trainer not found." });
            }

            return Ok(new { message = "Trainer deleted successfully.", room = trainerToDelete });
        }

        [HttpPut("{jmbg}")]
        public async Task<ActionResult<TrainerAddDto>> UpdateClient(string jmbg, [FromBody] TrainerUpdateDto trainerUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedTrainer = await _trainerService.UpdateTrainerAsync(jmbg, trainerUpdateDto);
                return Ok(updatedTrainer);
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
