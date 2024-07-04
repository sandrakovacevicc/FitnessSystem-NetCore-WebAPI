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
    public class TrainerRepository : Repository<Trainer>,ITrainerRepository
    {
       

        public TrainerRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Trainer> CreateAsync(Trainer trainer)
        {
            await _dbContext.Trainers.AddAsync(trainer);
            await _dbContext.SaveChangesAsync();
            return trainer;
        }

        public async Task<Trainer> DeleteAsync(int id)
        {
            var trainer = await _dbContext.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return null;
            }

            _dbContext.Trainers.Remove(trainer);
            await _dbContext.SaveChangesAsync();

            return trainer;
        }

        public async Task<Trainer?> GetByIdAsync(int id)
        {
            return await _dbContext.Trainers.FirstOrDefaultAsync(t => t.UserId == id);
        }
    }
}
