using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessSystem.Presentation.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientDto>>> GetAll()
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet("{jmbg}")]
        public async Task<IActionResult> GetById(string jmbg)
        {
            var clientDto = await _clientService.GetByIdAsync(jmbg);
            if (clientDto == null)
            {
                return NotFound();
            }
            return Ok(clientDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientAddDto clientAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdClient = await _clientService.CreateClientAsync(clientAddDto); 
                return Ok(createdClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the client.");
            }
        }

        [HttpDelete("{jmbg}")]
        public async Task<IActionResult> DeleteClient(string jmbg)
        {
            var clientToDelete = await _clientService.DeleteClientAsync(jmbg);
            if (clientToDelete == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new { message = "User deleted successfully.", client = clientToDelete });
        }

        [HttpPut("{jmbg}")]
        public async Task<ActionResult<ClientDto>> UpdateClient(string jmbg, [FromBody] ClientUpdateDto clientUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedClient = await _clientService.UpdateClientAsync(jmbg, clientUpdateDto);
                return Ok(updatedClient);
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
