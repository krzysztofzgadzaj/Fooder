using Fooder.Dto.ViewModel.Feed;
using Fooder.Persistence.Entities;
using Fooder.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fooder.Services.Review
{
    public interface IReviewService : IBaseService<ReviewEntity, ReviewViewModel>
    {
        Task<ICollection<ReviewViewModel>> GetRangeAsync(int feedId);
    }
}

