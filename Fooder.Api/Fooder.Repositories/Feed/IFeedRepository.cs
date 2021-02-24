using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;
using System.Threading.Tasks;

namespace Fooder.Repositories.Feed
{
    public interface IFeedRepository : IBaseRepository<FeedEntity>
    {
        Task<FeedEntity> GetFeedWithAfflictionsById(int id);
    }
}
