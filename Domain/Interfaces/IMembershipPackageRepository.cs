using Core.Entities;
using FitnessSystem.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMembershipPackageRepository : IRepository<MembershipPackage>
    {
        //Task<List<MembershipPackage>> GetAllAsync();
        Task<MembershipPackage> GetByIdAsync(int id);
        Task<MembershipPackage> CreateAsync(MembershipPackage package);
    }
}
