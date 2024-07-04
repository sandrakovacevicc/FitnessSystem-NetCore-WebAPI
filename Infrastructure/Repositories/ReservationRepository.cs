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
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            await _dbContext.Reservations.AddAsync(reservation);
            await _dbContext.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> DeleteAsync(int id)
        {
            var reservation = await _dbContext.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return null;
            }

            _dbContext.Reservations.Remove(reservation);
            await _dbContext.SaveChangesAsync();

            return reservation;
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
                .FirstOrDefaultAsync(r => r.ReservationId == id);
        }

    }
}
