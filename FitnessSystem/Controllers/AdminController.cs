using FitnessSystem.Application.DTOs.Admin;
using FitnessSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace FitnessSystem.Presentation.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger; 

        public AdminController(IAdminService adminService, ILogger<AdminController> logger) // Dodajte logger u konstruktor
        {
            _adminService = adminService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<AdminDto>>> GetAll()
        {
                _logger.LogInformation("Fetching all admins.");
                var admins = await _adminService.GetAllAsync();
                return Ok(admins);
            
        }

        [HttpGet("{jmbg}")]
        public async Task<IActionResult> GetById(string jmbg)
        {
            _logger.LogInformation($"Fetching admin with JMBG: {jmbg}");
            var adminDto = await _adminService.GetByIdAsync(jmbg);
            if (adminDto == null)
            {
                _logger.LogWarning($"Admin with JMBG: {jmbg} not found.");
                return NotFound();
            }
            return Ok(adminDto);
        }

    }
}
