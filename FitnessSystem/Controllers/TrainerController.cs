using FitnessSystem.Application.DTOs.Trainer;
using FitnessSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<List<TrainerDto>>> GetAllTrainers()
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
                return NotFound(new { message = "Trainer not found." });
            }
            return Ok(trainerDto);
        }

        [HttpPut("{jmbg}")]
        public async Task<ActionResult<TrainerAddDto>> UpdateTrainer(string jmbg, [FromBody] TrainerUpdateDto trainerUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTrainer = await _trainerService.UpdateTrainerAsync(jmbg, trainerUpdateDto);
            if (updatedTrainer == null)
            {
                return NotFound(new { message = "Trainer not found." });
            }

            return Ok(updatedTrainer);
        }
    }
}
