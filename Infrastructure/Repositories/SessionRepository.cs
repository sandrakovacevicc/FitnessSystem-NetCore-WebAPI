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
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _appDbContext;

        public SessionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Session>> GetAllAsync()
        {
            return await _appDbContext.Sessions.ToListAsync();

        }
    }
}
