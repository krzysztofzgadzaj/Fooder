using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Persistence.Entities;

namespace Fooder.Repositories.Afflictions
{
    public interface IAfflictionRepository
    {
        Task<ICollection<AfflictionEntity>> GetRangeAsync(CancellationToken cancellationToken);
    }
}
