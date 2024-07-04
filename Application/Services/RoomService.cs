using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IRoomRepository roomRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<RoomDto> CreateRoomAsync(RoomDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _roomRepository.CreateAsync(room);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return _mapper.Map<RoomDto>(room);
        }

        public async Task<List<RoomDto>> GetAllAsync()
        {
            var rooms =  _roomRepository.GetAll().ToList();

            var roomsDto = _mapper.Map<List<RoomDto>>(rooms);

            return roomsDto;

        }

        public async Task<RoomDto> GetByIdAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            return _mapper.Map<RoomDto>(room);
        }
    }
}
