using Fooder.Dto.Request.Feed.FeedMark;
using Fooder.Dto.ViewModel.Feed;
using Fooder.Persistence.Entities;
using Fooder.Services.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fooder.Services.Feed
{
    public interface IFeedService : IBaseService<FeedEntity, FeedViewModel>
    {
        Task<IEnumerable<FeedViewModel>> GetAsync(CancellationToken cancellationToken, string userName);
        Task<FeedMarkViewModel> AddFeedMarkAsync(CreateFeedMarkRequest request);
        Task<FeedMarkViewModel> UpdateFeedMarkAsync(UpdateFeedMarkRequest request, int id);
        Task DeleteFeedMarkById(int id);
    }
}
