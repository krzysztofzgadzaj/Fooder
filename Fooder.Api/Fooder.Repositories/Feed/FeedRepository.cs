using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fooder.Persistence.Contexts;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Fooder.Repositories.Feed
{
    public sealed class FeedRepository : BaseRepository<FeedEntity, FooderContext>, IFeedRepository
    {
        public FeedRepository(FooderContext context)
            : base(context)
        {
        }

        public override async Task<ICollection<FeedEntity>> GetAsync(CancellationToken cancellationToken)
        {
            var entity = await DbContext.Feeds
                .Include(feed => feed.BrandName)
                .Include(feed => feed.Comments)
                //.Include(feed => feed.Reviews)
                .Include(feed => feed.DogAfflictions)
                    .ThenInclude(affliction => affliction.AfflictionEntity)
                .ToListAsync(cancellationToken);

            return entity;
        }

        public async Task<FeedEntity> GetFeedWithAfflictionsById(int id) =>
            await DbContext.Feeds
                .Include(feed => feed.DogAfflictions)
                    .ThenInclude(affliction => affliction.AfflictionEntity)
                .FirstOrDefaultAsync(feed => 
                    feed.Id == id);
    }
}
