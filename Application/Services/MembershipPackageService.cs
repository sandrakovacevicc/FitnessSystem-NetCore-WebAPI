using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.MembershipPackage;
using FitnessSystem.Application.DTOs.Reservation;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Services
{
    public class MembershipPackageService : IMembershipPackageService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MembershipPackageService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MembershipPackageDto>> GetAllAsync()
        {
            var membershipPackages =  _unitOfWork.MembershipPackages.GetAll().ToList();
            return _mapper.Map<List<MembershipPackageDto>>(membershipPackages);
        }

        public async Task<MembershipPackageDto> GetByIdAsync(int id)
        {
            var membershipPackage = await _unitOfWork.MembershipPackages.GetByIdAsync(id);
            return _mapper.Map<MembershipPackageDto>(membershipPackage);
        }

        public async Task<MembershipPackageUpdateDto> UpdateMembershipPackageAsync(int id, MembershipPackageUpdateDto membershipPackageUpdateDto)
        {
            var membershipPackage = await _unitOfWork.MembershipPackages.GetByIdAsync(id);
            if (membershipPackage == null)
            {
                throw new KeyNotFoundException("Membership package not found.");
            }

            _mapper.Map(membershipPackageUpdateDto, membershipPackage);
            await _unitOfWork.MembershipPackages.UpdateAsync(membershipPackage, id);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<MembershipPackageUpdateDto>(membershipPackage);
        }
    }
}
