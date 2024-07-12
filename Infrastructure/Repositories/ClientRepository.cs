using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Data;
using FitnessSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FitnessSystem.Infrastructure.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<Client?> GetByIdAsync(string jmbg)
        {
            return await _dbContext.Clients.Include(c => c.MembershipPackage).FirstOrDefaultAsync(c => c.JMBG == jmbg);
        }
    }
}
