using FitnessSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Interfaces
{
    public interface IRoomService
    {
        Task<List<RoomDto>> GetAllAsync();
        Task<RoomDto> GetByIdAsync(int id);
        Task<RoomDto> CreateRoomAsync(RoomDto roomDto);
    }
}
