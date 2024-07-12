using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessSystem.Core.Interfaces.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(string includeProperties = "");
        Task UpdateAsync(TEntity entityToUpdate, object key);
        Task CreateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(object id);
        Task<TEntity> GetByIdAsync(object id);

    }
}
