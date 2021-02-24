using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fooder.Repositories.Base
{
    public abstract class BaseRepository<T, U> : IBaseRepository<T> where T : BaseEntity where U : DbContext
    {
        protected BaseRepository(U context)
        {
            DbContext = context;
        }

        protected U DbContext { get; }

        public virtual async Task<T> AddAsync(T entity) =>
            (await DbContext.Set<T>()
                .AddAsync(entity)).Entity;

        public virtual async Task AddRangeAsync(IEnumerable<T> entities) =>
            await DbContext
                .Set<T>()
                .AddRangeAsync(entities);

        public virtual async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            DbContext.Set<T>()
                .Remove(entity);
        }

        public virtual async Task<ICollection<T>> GetAsync() =>
            await GetAsync(CancellationToken.None);

        public virtual async Task<ICollection<T>> GetAsync(CancellationToken cancellationToken) =>
            await DbContext
                .Set<T>()
                .ToListAsync(cancellationToken);

        public virtual async Task<T> GetByIdAsync(int id) =>
            await GetByIdAsync(id, CancellationToken.None);

        public virtual async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken) =>
            await DbContext
                .Set<T>()
                .FirstOrDefaultAsync(entity =>
                    entity.Id == id,
                    cancellationToken);

        public virtual T Update(T entity) =>
            DbContext.Set<T>()
                .Update(entity)
                .Entity;

        public virtual async Task<int> SaveChangesAsync() =>
            await DbContext.SaveChangesAsync();
    }
}
