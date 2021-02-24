using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Persistence.Contexts;
using Fooder.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fooder.Repositories.Afflictions
{
    public sealed class AfflictionRepository : IAfflictionRepository
    {
        private readonly FooderContext _context;

        public AfflictionRepository(FooderContext context)
        {
            _context = context;
        }


        public async Task<ICollection<AfflictionEntity>> GetRangeAsync(CancellationToken cancellationToken) =>
            await _context
                .Afflictions
                .AsNoTracking()
                .ToListAsync(cancellationToken);
    }
}
