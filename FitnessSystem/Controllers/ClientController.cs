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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var clientDto = await _clientService.GetByIdAsync(id);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var clientToDelete = await _clientService.DeleteClientAsync(id);
            if (clientToDelete == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new { message = "User deleted successfully.", client = clientToDelete });
        }
    }
}
