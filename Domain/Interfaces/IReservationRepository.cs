using Core.Entities;
using FitnessSystem.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        //Task<List<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int id);
    }
}
