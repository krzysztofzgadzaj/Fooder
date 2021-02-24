using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fooder.Repositories.Review
{
    public interface IReviewRepository : IBaseRepository<ReviewEntity>
    {
        Task<ICollection<ReviewEntity>> GetFeedReviewsAsync(int feedId);
    }
}

