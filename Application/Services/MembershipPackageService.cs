using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.MembershipPackage;
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

        public async Task<MembershipPackageDto> CreateMembershipPackageAsync(MembershipPackageDto membershipPackageDto)
        {
            var membershipPackage = _mapper.Map<MembershipPackage>(membershipPackageDto);
            await _unitOfWork.MembershipPackages.CreateAsync(membershipPackage);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<MembershipPackageDto>(membershipPackage);
        }

        public async Task<MembershipPackageDeleteDto> DeleteMembershipPackageAsync(int id)
        {
            var membershipPackage = await _unitOfWork.MembershipPackages.GetByIdAsync(id);
            if (membershipPackage == null)
            {
                return null;
            }

            await _unitOfWork.MembershipPackages.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<MembershipPackageDeleteDto>(membershipPackage);
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

        public async Task<MembershipPackageDeleteDto> UpdateMembershipPackageAsync(int id, MembershipPackageDto membershipPackageDto)
        {
            var membershipPackage = await _unitOfWork.MembershipPackages.GetByIdAsync(id);
            if (membershipPackage == null)
            {
                throw new KeyNotFoundException("Membership package not found.");
            }

            _mapper.Map(membershipPackageDto, membershipPackage);
            await _unitOfWork.MembershipPackages.UpdateAsync(membershipPackage, id);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<MembershipPackageDeleteDto>(membershipPackage);
        }
    }
}
