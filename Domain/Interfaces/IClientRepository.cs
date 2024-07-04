using Core.Entities;
using FitnessSystem.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        //Task<List<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(int id);
        Task<Client> CreateAsync(Client client);
    }
}
