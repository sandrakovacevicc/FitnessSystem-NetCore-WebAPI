using Core.Entities;
using FitnessSystem.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        //Task<List<Session>> GetAllAsync();
        Task<Session> GetByIdAsync(int id);
    }
}
