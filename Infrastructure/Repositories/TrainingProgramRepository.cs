using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Data;
using FitnessSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FitnessSystem.Infrastructure.Repositories
{
    public class TrainingProgramRepository : Repository<TrainingProgram>, ITrainingProgramRepository
    {
        public TrainingProgramRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<TrainingProgram?> GetByIdAsync(int id)
        {
            return await _dbContext.TrainingPrograms.FirstOrDefaultAsync(t => t.TrainingProgramId == id);
        }
    }
}
