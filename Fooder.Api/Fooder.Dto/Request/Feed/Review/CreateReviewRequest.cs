using Fooder.Persistence.Entities;

namespace Fooder.Dto.Request.Feed.Review
{
    public sealed class CreateReviewRequest : BaseReviewRequest
    {
        public int AuthorId { get; set; }

        public override ReviewEntity CreateEntity() =>
            new ReviewEntity
            {
                Content = Content,
                AuthorId = AuthorId,
                FeedEntityId = FeedId,
            };
    }
}
