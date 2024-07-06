using FitnessSystem.Core.Interfaces.Base;
using FitnessSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessSystem.Infrastructure.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll(string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query;
        }

        public async Task UpdateAsync(TEntity entityToUpdate, object key)
        {
            if (entityToUpdate == null)
                throw new ArgumentNullException(nameof(entityToUpdate));

            var existingEntity = await _dbContext.Set<TEntity>().FindAsync(key);
            if (existingEntity != null)
            {
                _dbContext.Entry(existingEntity).CurrentValues.SetValues(entityToUpdate);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }



    }
}
