using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Data;
using FitnessSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Infrastructure.Repositories
{
    public class MembershipPackageRepository : Repository<MembershipPackage>, IMembershipPackageRepository
    {
        public MembershipPackageRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<MembershipPackage> CreateAsync(MembershipPackage package)
        {
            await _dbContext.MembershipPackages.AddAsync(package);
            await _dbContext.SaveChangesAsync();
            return package;
        }

        //public async Task<List<MembershipPackage>> GetAllAsync()
        //{
        //    return await _dbContext.MembershipPackages.ToListAsync();

        //}

        public async Task<MembershipPackage?> GetByIdAsync(int id)
        {
            return await _dbContext.MembershipPackages.FirstOrDefaultAsync(m => m.MembershipPackageId == id);
        }
    }
}
