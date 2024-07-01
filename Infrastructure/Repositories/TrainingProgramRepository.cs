using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Infrastructure.Repositories
{
    public class TrainingProgramRepository : ITrainingProgramRepository
    {
        private readonly AppDbContext _appDbContext;

        public TrainingProgramRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<TrainingProgram>> GetAllAsync()
        {
            return await _appDbContext.TrainingPrograms.ToListAsync();
        }
    }
}
