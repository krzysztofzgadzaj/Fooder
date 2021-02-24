using Fooder.Persistence.Contexts;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fooder.Repositories.Review
{
    public sealed class ReviewRepository : BaseRepository<ReviewEntity, FooderContext>,
        IReviewRepository
    {
        public ReviewRepository(FooderContext context)
            : base(context)
        {
        }

        public async Task<ICollection<ReviewEntity>> GetFeedReviewsAsync(int feedId) =>
            await DbContext.Reviews
                .Where(review =>
                    review.FeedEntityId == feedId)
                .ToListAsync();
    }
}

