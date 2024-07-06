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
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<Client> DeleteAsync(string jmbg)
        {
            var client = await _dbContext.Clients.FindAsync(jmbg);
            if (client == null)
            {
                return null;
            }

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();

            return client;
        }

        public async Task<Client?> GetByIdAsync(string jmbg)
        {
            return await _dbContext.Clients.Include(c => c.MembershipPackage).FirstOrDefaultAsync(c => c.JMBG == jmbg);
        }
    }
}
