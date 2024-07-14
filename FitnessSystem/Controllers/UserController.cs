using FitnessSystem.Application.DTOs.User;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessSystem.Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }


        [HttpGet("{jmbg}")]
        public async Task<IActionResult> GetById(string jmbg)
        {
            var userDto = await _userService.GetByIdAsync(jmbg);
            if (userDto == null)
            {
                return NotFound();
            }
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdUser = await _userService.CreateUserAsync(userDto);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while creating the user.");
            }
        }


        [HttpDelete("{jmbg}")]
        public async Task<IActionResult> DeleteUser(string jmbg)
        {
            var userToDelete = await _userService.DeleteUserAsync(jmbg);
            if (userToDelete == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new { message = "User deleted successfully.", user = userToDelete });
        }

        [HttpPut("{jmbg}")]
        public async Task<ActionResult<UserDto>> UpdateUser(string jmbg, [FromBody] UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedUser = await _userService.UpdateUserAsync(jmbg, userUpdateDto);
                return Ok(updatedUser);
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
