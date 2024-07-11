using FitnessSystem.Application.DTOs.Room;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var roomToDelete = await _roomService.DeleteRoomAsync(id);
            if (roomToDelete == null)
            {
                return NotFound(new { message = "Room not found." });
            }

            return Ok(new { message = "Room deleted successfully.", room = roomToDelete });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoomDeleteDto>> UpdateClient(int id, [FromBody] RoomDto roomDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedRoom = await _roomService.UpdateRoomAsync(id, roomDto);
                return Ok(updatedRoom);
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
