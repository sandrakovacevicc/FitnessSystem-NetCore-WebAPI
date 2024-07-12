using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.User;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _unitOfWork.Users.CreateAsync(user);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDeleteDto> DeleteUserAsync(string JMBG)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(JMBG);
            if (user == null)
            {
                return null;
            }

            await _unitOfWork.Users.DeleteAsync(JMBG);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<UserDeleteDto>(user);
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = _unitOfWork.Users.GetAll().ToList();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(string JMBG)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(JMBG);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserAsync(string jmbg, UserUpdateDto userUpdateDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(jmbg);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            user.Name = userUpdateDto.Name;
            user.Surname = userUpdateDto.Surname;
            user.Email = userUpdateDto.Email;
            await _unitOfWork.Users.UpdateAsync(user, jmbg);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}
