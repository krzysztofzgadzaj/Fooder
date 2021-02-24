using Fooder.Persistence.Entities;

namespace Fooder.Dto.Request.Feed.Review
{
    public  class BaseReviewRequest : ICommandRequest<ReviewEntity>
    {
        public int FeedId { get; set; }
        public string Content { get; set; }
        
        public virtual ReviewEntity CreateEntity() =>
            new ReviewEntity
            {
                Content = Content,
                FeedEntityId = FeedId,
            };
    }
}
