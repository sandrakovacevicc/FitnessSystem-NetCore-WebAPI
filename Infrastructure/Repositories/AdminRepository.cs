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

        public async Task<Admin> CreateAsync(Admin admin)
        {
            await _dbContext.Admins.AddAsync(admin);
            await _dbContext.SaveChangesAsync();
            return admin;
        }


        //public async Task<List<Admin>> GetAllAsync()
        //{
        //    return await _dbContext.Admins.ToListAsync();

        //}

        public async Task<Admin?> GetByIdAsync(int id)
        {
            return await _dbContext.Admins.FirstOrDefaultAsync(a => a.UserId == id);
        }

        
    }
}
