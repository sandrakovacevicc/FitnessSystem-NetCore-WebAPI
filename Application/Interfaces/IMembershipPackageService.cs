using FitnessSystem.Application.DTOs.MembershipPackage;
using FitnessSystem.Application.DTOs.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Interfaces
{
    public interface IMembershipPackageService
    {
        Task<List<MembershipPackageDto>> GetAllAsync();
        Task<MembershipPackageDto> GetByIdAsync(int id);
        Task<MembershipPackageDto> CreateMembershipPackageAsync(MembershipPackageDto membershipPackageDto);
        Task<MembershipPackageDeleteDto> DeleteMembershipPackageAsync(int id);
        Task<MembershipPackageDeleteDto> UpdateMembershipPackageAsync(int id, MembershipPackageDto membershipPackageDto);
    }
}
