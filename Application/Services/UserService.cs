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
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _userRepository.CreateAsync(user);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users =  _userRepository.GetAll();

            var usersDto = _mapper.Map<List<UserDto>>(users);

            return usersDto;
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }
    }
}
