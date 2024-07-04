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

        public async Task<Client> CreateAsync(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();
            return client;
        }

        //public async Task<List<Client>> GetAllAsync()
        //{
        //    return await _dbContext.Clients
        //.Include(c => c.MembershipPackage)
        //.ToListAsync();

        //}

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _dbContext.Clients.Include(c => c.MembershipPackage).FirstOrDefaultAsync(c => c.UserId == id);
        }
    }
}
