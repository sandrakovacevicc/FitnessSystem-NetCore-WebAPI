﻿using AutoMapper;
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
    public class MembershipPackageService : IMembershipPackageService
    {
        private readonly IMapper _mapper;
        private readonly IMembershipPackageRepository _membershipPackageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MembershipPackageService(IMapper mapper, IMembershipPackageRepository membershipPackageRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _membershipPackageRepository = membershipPackageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<MembershipPackageDto> CreateMembershipPackageAsync(MembershipPackageDto membershipPackageDto)
        {
            var membershipPackage = _mapper.Map<MembershipPackage>(membershipPackageDto);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _membershipPackageRepository.CreateAsync(membershipPackage);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return _mapper.Map<MembershipPackageDto>(membershipPackage);
        }

        public async Task<List<MembershipPackageDto>> GetAllAsync()
        {
            var membershipPackages = _membershipPackageRepository.GetAll().ToList();
            
            var membershipPackagesDto = _mapper.Map<List<MembershipPackageDto>>(membershipPackages);

            return membershipPackagesDto;
        }

        public async Task<MembershipPackageDto> GetByIdAsync(int id)
        {
            var membershipPackage = await _membershipPackageRepository.GetByIdAsync(id);
            return _mapper.Map<MembershipPackageDto>(membershipPackage);
        }
    }
}
