using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Infrastructure.Repositories
{
    public class MembershipPackageRepository : IMembershipPackageRepository
    {
        private readonly AppDbContext _appDbContext;

        public MembershipPackageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<MembershipPackage>> GetAllAsync()
        {
            return await _appDbContext.MembershipPackages.ToListAsync();
            
        }
    }
}
