using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessSystem.Presentation.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoomDto>>> GetAll()
        {
            var rooms = await _roomService.GetAllAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var roomDto = await _roomService.GetByIdAsync(id);
            if (roomDto == null)
            {
                return NotFound();
            }
            return Ok(roomDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomDto roomDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdRoom = await _roomService.CreateRoomAsync(roomDto);
                return Ok(createdRoom);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while creating the admin.");
            }
        }
    }
}
