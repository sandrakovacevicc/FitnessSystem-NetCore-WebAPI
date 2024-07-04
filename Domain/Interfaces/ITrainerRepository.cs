using Core.Entities;
using FitnessSystem.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITrainerRepository : IRepository<Trainer>
    {
        Task<Trainer> GetByIdAsync(int id);
        Task<Trainer> CreateAsync(Trainer trainer);
        Task<Trainer> DeleteAsync(int id);
    }
}
