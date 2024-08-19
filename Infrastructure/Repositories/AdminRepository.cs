using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Data;
using FitnessSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public AdminRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Admin?> GetByIdAsync(string jmbg)
        {
            return await _dbContext.Admins.FirstOrDefaultAsync(a => a.JMBG == jmbg);
        }
    }
}
