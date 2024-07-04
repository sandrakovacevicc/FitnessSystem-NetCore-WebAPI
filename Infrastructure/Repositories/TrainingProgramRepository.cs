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
    public class TrainingProgramRepository : Repository<TrainingProgram>, ITrainingProgramRepository
    {
        public TrainingProgramRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TrainingProgram> CreateAsync(TrainingProgram trainingProgram)
        {
            await _dbContext.TrainingPrograms.AddAsync(trainingProgram);
            await _dbContext.SaveChangesAsync();
            return trainingProgram;
        }

        //public async Task<List<TrainingProgram>> GetAllAsync()
        //{
        //    return await _dbContext.TrainingPrograms.Include(t => t.Sessions).ToListAsync();
        //}

        public async Task<TrainingProgram?> GetByIdAsync(int id)
        {
            return await _dbContext.TrainingPrograms.FirstOrDefaultAsync(t => t.TrainingProgramId == id);
        }
    }
}
