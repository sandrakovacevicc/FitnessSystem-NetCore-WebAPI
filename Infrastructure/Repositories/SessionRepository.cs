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


        public async Task<Session> DeleteAsync(int id)
        {
            var session = await _dbContext.Sessions.FindAsync(id);
            if (session == null)
            {
                return null;
            }

            _dbContext.Sessions.Remove(session);
            await _dbContext.SaveChangesAsync();

            return session;
        }

        public async Task<Session?> GetByIdAsync(int id)
        {
            return await _dbContext.Sessions.Include(s => s.Trainer).Include(s => s.TrainingProgram).Include(s => s.Room).FirstOrDefaultAsync(s => s.SessionId == id);
        }
    }
}
