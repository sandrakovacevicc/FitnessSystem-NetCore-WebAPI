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

       
    }
}
