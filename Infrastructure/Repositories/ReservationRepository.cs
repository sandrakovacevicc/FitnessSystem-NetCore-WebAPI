using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Data;
using FitnessSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FitnessSystem.Infrastructure.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _dbContext.Reservations
                .Include(r => r.Client)
                .ThenInclude(c => c.MembershipPackage)
                .Include(r => r.Session)
                    .ThenInclude(s => s.TrainingProgram)
                .Include(r => r.Session)
                    .ThenInclude(s => s.Room)
                .Include(r => r.Session)
                    .ThenInclude(s => s.Trainer)
                .FirstOrDefaultAsync(r => r.ReservationId == id);
        }
    }
}
