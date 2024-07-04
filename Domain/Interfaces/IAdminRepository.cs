using Core.Entities;
using FitnessSystem.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAdminRepository :  IRepository<Admin>
    {
        //Task<List<Admin>> GetAllAsync();
        Task<Admin> GetByIdAsync(int id);
        Task<Admin> CreateAsync(Admin admin);
    }
}
