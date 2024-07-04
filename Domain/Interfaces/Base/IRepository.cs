using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessSystem.Core.Interfaces.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(string includeProperties = "");
        
    }
}
