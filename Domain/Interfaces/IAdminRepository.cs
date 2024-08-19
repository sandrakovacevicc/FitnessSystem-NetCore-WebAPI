using Core.Entities;
using FitnessSystem.Core.Interfaces.Base;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<Admin?> GetByIdAsync(string jmbg);
    }
}
