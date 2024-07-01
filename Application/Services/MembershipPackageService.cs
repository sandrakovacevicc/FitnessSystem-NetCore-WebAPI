using AutoMapper;
using Core.Interfaces;
using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
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

        public MembershipPackageService(IMapper mapper, IMembershipPackageRepository membershipPackageRepository)
        {
            _mapper = mapper;
            _membershipPackageRepository = membershipPackageRepository;
        }
        public async Task<List<MembershipPackageDto>> GetAllAsync()
        {
            var membershipPackages = await _membershipPackageRepository.GetAllAsync();
            
            var membershipPackagesDto = _mapper.Map<List<MembershipPackageDto>>(membershipPackages);

            return membershipPackagesDto;
        }
    }
}
