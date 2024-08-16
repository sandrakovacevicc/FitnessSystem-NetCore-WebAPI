using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Data;
using FitnessSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessSystem.Infrastructure.Repositories
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        public SessionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

       
        public async Task<Session?> GetByIdAsync(int id)
        {
            return await _dbContext.Sessions.Include(s => s.Trainer).Include(s => s.TrainingProgram).Include(s => s.Room).FirstOrDefaultAsync(s => s.SessionId == id);
        }
    }
}
