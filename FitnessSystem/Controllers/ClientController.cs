using FitnessSystem.Application.DTOs.Client;
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

        [HttpGet("search")]
        public async Task<IActionResult> SearchClients([FromQuery] string searchTerm)
        {
            var clients = await _clientService.SearchClientsAsync(searchTerm);
            return Ok(clients);
        }
    }
}
