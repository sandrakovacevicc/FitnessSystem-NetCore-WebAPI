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

namespace Infrastructure.Repositories
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public AdminRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<Admin>? DeleteAsync(string jmbg)
        {
            var admin = await _dbContext.Admins.FindAsync(jmbg);
            if (admin == null)
            {
                return null;
            }

            _dbContext.Admins.Remove(admin);
            await _dbContext.SaveChangesAsync();

            return admin;
        }

        public async Task<Admin?> GetByIdAsync(string jmbg)
        {
            return await _dbContext.Admins.FirstOrDefaultAsync(a => a.JMBG == jmbg);
        }

        
    }
}
