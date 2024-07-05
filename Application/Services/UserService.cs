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

        public async Task<UserDeleteDto> DeleteUserAsync(string JMBG)
        {
            var user = await _userRepository.GetByIdAsync(JMBG);
            if (user == null)
            {
                return null;
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var deletedUser = await _userRepository.DeleteAsync(JMBG);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<UserDeleteDto>(deletedUser);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }



        public async Task<List<UserDto>> GetAllAsync()
        {
            var users =  _userRepository.GetAll();

            var usersDto = _mapper.Map<List<UserDto>>(users);

            return usersDto;
        }

        public async Task<UserDto> GetByIdAsync(string JMBG)
        {
            var user = await _userRepository.GetByIdAsync(JMBG);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserAsync(string jmbg, UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.GetByIdAsync(jmbg);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            user.Name = userUpdateDto.Name;
            user.Surname = userUpdateDto.Surname;
            user.Email = userUpdateDto.Email;
            user.Password = userUpdateDto.Password;
 

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _userRepository.UpdateAsync(user, jmbg);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

               
                var userDto = new UserDto
                {
                    JMBG = user.JMBG, 
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Password = user.Password
                   
                };

                return userDto;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

    }
}
