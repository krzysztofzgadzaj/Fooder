using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Persistence.Entities;

namespace Fooder.Repositories.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<ICollection<T>> GetAsync(CancellationToken cancellationToken);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task DeleteByIdAsync(int id);
        T Update(T model);
        Task<int> SaveChangesAsync();
    }
}
