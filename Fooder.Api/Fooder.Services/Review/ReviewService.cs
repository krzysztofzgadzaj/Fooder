using Fooder.Dto.ViewModel.Feed;
using Fooder.Persistence.Entities;
using Fooder.Repositories.Review;
using Fooder.Services.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fooder.Helpers.Identity;

namespace Fooder.Services.Review
{
    public sealed class ReviewService : BaseService<IReviewRepository, ReviewEntity, ReviewViewModel>,
        IReviewService
    {
        private readonly IIdentityPort _identityPort;

        public ReviewService(IIdentityPort identityPort,
            IReviewRepository repository)
                : base(repository)
        {
            _identityPort = identityPort;
        }

        public async Task<ICollection<ReviewViewModel>> GetRangeAsync(int feedId)
        {
            var reviews = await Repository.GetFeedReviewsAsync(feedId);

            var reviewViewModels = await Task.WhenAll(reviews.Select(
                    async review => new ReviewViewModel
                    {
                        Id = review.Id,
                        Author = (await _identityPort.GetUserAsync(review.AuthorId))?.Login,
                        Content = review.Content,
                    })
                .ToList());

            return reviewViewModels;
        }
    }
}

