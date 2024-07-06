using Core.Entities;
using FitnessSystem.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<Room> GetByIdAsync(int id);
        Task<Room> DeleteAsync(int id);
    }
}
