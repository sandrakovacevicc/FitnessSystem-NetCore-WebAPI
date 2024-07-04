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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IAdminRepository adminRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AdminDto> CreateAdminAsync(AdminDto adminDto)
        {
            var admin = _mapper.Map<Admin>(adminDto);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _adminRepository.CreateAsync(admin);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return _mapper.Map<AdminDto>(admin);
        }

        public async Task<List<AdminDto>> GetAllAsync()
        {
            var admins =  _adminRepository.GetAll().ToList();

            var adminsDto = _mapper.Map<List<AdminDto>>(admins);

            return adminsDto;
        }

        public async Task<AdminDto> GetByIdAsync(int id)
        {
            var admin = await _adminRepository.GetByIdAsync(id);
            return _mapper.Map<AdminDto>(admin);
        }
    }
}
