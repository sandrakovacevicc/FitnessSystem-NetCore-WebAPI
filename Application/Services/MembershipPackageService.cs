using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.MembershipPackage;
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

        public async Task<MembershipPackageDeleteDto> DeleteMembershipPackageAsync(int id)
        {
            var membershipPackage = await _membershipPackageRepository.GetByIdAsync(id);
            if (membershipPackage == null)
            {
                return null;
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var deletedMembershipPackage = await _membershipPackageRepository.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<MembershipPackageDeleteDto>(deletedMembershipPackage);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
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

        public async Task<MembershipPackageDeleteDto> UpdateMembershipPackageAsync(int id, MembershipPackageDto membershipPackageDto)
        {
            var membershipPackage = await _membershipPackageRepository.GetByIdAsync(id);
            if (membershipPackage == null)
            {
                throw new KeyNotFoundException("Membership package not found.");
            }

            membershipPackage.Description = membershipPackageDto.Description;
            membershipPackage.NumberOfMonths = membershipPackageDto.NumberOfMonths;
            membershipPackage.Price = membershipPackageDto.Price;
            membershipPackage.Name = membershipPackageDto.Name;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _membershipPackageRepository.UpdateAsync(membershipPackage, id);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                var membershipPackageUpdateDto = new MembershipPackageDeleteDto
                {
                    MembershipPackageId = membershipPackage.MembershipPackageId,
                    Name = membershipPackage.Name,
                    Description = membershipPackage.Description,
                    NumberOfMonths = membershipPackage.NumberOfMonths,
                    Price = membershipPackage.Price
                };

                return membershipPackageUpdateDto;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
