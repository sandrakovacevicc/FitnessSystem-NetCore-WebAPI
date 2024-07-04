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
    public class SessionRepository : Repository<Session>,ISessionRepository
    {
        public SessionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        //public async Task<List<Session>> GetAllAsync()
        //{
        //    return await _dbContext.Sessions
        //        .Include(s => s.Clients).ThenInclude(s => s.MembershipPackage).
        //        Include(s => s.Room).
        //        Include(s => s.TrainingProgram).ToListAsync();

        //}

        public async Task<Session?> GetByIdAsync(int id)
        {
            return await _dbContext.Sessions.FirstOrDefaultAsync(s => s.SessionId == id);
        }
    }
}
