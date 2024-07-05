using Core.Entities;
using FitnessSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(string JMBG);
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task<UserDeleteDto> DeleteUserAsync(string JMBG);
        Task<UserDto> UpdateUserAsync(string jmbg, UserUpdateDto userUpdateDto);

    }
}
