using AutoMapper;
using Core.Entities;
using FitnessSystem.Application.DTOs.Admin;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AdminDto> CreateAdminAsync(AdminDto adminDto)
        {
            var admin = _mapper.Map<Admin>(adminDto);
            await _unitOfWork.Admins.CreateAsync(admin);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<AdminDto>(admin);
        }

        public async Task<AdminDeleteDto> DeleteAdminAsync(string jmbg)
        {
            var admin = await _unitOfWork.Admins.GetByIdAsync(jmbg);
            if (admin == null)
            {
                return null;
            }

            await _unitOfWork.Admins.DeleteAsync(jmbg);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<AdminDeleteDto>(admin);
        }

        public async Task<List<AdminDto>> GetAllAsync()
        {
            var admins = await _unitOfWork.Admins.GetAll().ToListAsync();
            return _mapper.Map<List<AdminDto>>(admins);
        }

        public async Task<AdminDto> GetByIdAsync(string jmbg)
        {
            var admin = await _unitOfWork.Admins.GetByIdAsync(jmbg);
            return _mapper.Map<AdminDto>(admin);
        }

        public async Task<AdminDto> UpdateAdminAsync(string jmbg, AdminUpdateDto adminUpdateDto)
        {
            var admin = await _unitOfWork.Admins.GetByIdAsync(jmbg);
            if (admin == null)
            {
                throw new KeyNotFoundException("Admin not found.");
            }

            admin.Name = adminUpdateDto.Name;
            admin.Surname = adminUpdateDto.Surname;
            admin.Email = adminUpdateDto.Email;
            await _unitOfWork.Admins.UpdateAsync(admin, jmbg);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AdminDto>(admin);
        }
    }
}
