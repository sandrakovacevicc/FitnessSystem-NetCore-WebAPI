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

        //public async Task<List<Reservation>> GetAllAsync()
        //{
        //    return await _dbContext.Reservations
        //        //.Include(r => r.Client)
        //        .Include(r => r.Session)
        //        .Include(r => r.Session.Room)
        //        .Include(r => r.Session.TrainingProgram)
        //        .Include(r => r.Session.Trainer)
        //        .ToListAsync();

        //}

        public async Task<Reservation> GetByIdAsync(int id)
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
