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
    public class TrainerRepository : ITrainerRepository
    {
        private readonly AppDbContext _appDbContext;

        public TrainerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Trainer>> GetAllAsync()
        {
            return await _appDbContext.Trainers.ToListAsync();

        }
    }
}
