using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessSystem.Presentation.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AdminDto>>> GetAll()
        {
            var admins = await _adminService.GetAllAsync();
            return Ok(admins);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var adminDto = await _adminService.GetByIdAsync(id);
            if (adminDto == null)
            {
                return NotFound();
            }
            return Ok(adminDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminDto adminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdAdmin = await _adminService.CreateAdminAsync(adminDto);
                return Ok(createdAdmin);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while creating the admin.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var adminToDelete = await _adminService.DeleteAdminAsync(id);
            if (adminToDelete == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new { message = "User deleted successfully.", admin = adminToDelete });
        }
    }
}
