using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Data;
using FitnessSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FitnessSystem.Infrastructure.Repositories
{
    public class TrainerRepository : Repository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<Trainer?> GetByIdAsync(string jmbg)
        {
            return await _dbContext.Trainers.FirstOrDefaultAsync(t => t.JMBG == jmbg);
        }
    }
}
