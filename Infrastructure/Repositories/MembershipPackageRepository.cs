using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Data;
using FitnessSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FitnessSystem.Infrastructure.Repositories
{
    public class MembershipPackageRepository : Repository<MembershipPackage>, IMembershipPackageRepository
    {
        public MembershipPackageRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<MembershipPackage?> GetByIdAsync(int id)
        {
            return await _dbContext.MembershipPackages.FirstOrDefaultAsync(m => m.MembershipPackageId == id);
        }
    }
}
