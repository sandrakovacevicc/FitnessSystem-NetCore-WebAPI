using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.Room;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<RoomDto> CreateRoomAsync(RoomDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            await _unitOfWork.Rooms.CreateAsync(room);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<RoomDto>(room);
        }

        public async Task<RoomDeleteDto> DeleteRoomAsync(int id)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(id);
            if (room == null)
            {
                return null;
            }

            await _unitOfWork.Rooms.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<RoomDeleteDto>(room);
        }

        public async Task<List<RoomDto>> GetAllAsync()
        {
            var rooms = _unitOfWork.Rooms.GetAll().ToList();
            return _mapper.Map<List<RoomDto>>(rooms);
        }

        public async Task<RoomDto> GetByIdAsync(int id)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(id);
            return _mapper.Map<RoomDto>(room);
        }

        public async Task<RoomDto> UpdateRoomAsync(int id, RoomDto roomDto)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(id);
            if (room == null)
            {
                throw new KeyNotFoundException("Room not found.");
            }

            _mapper.Map(roomDto, room);
            await _unitOfWork.Rooms.UpdateAsync(room, id);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<RoomDto>(room);
        }
    }
}
