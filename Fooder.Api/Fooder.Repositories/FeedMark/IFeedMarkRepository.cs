using Fooder.Persistence.Entities;
using Fooder.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fooder.Repositories.FeedMark
{
    public interface IFeedMarkRepository : IBaseRepository<FeedMarkEntity>
    {
        Task<ICollection<FeedMarkEntity>> GetFeedMarksByUserNameAsync(string userName);
    }
}
