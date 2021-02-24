using Fooder.Persistence.Entities;

namespace Fooder.Dto.Request.Feed.FeedMark
{
    public class UpdateFeedMarkRequest : ICommandRequest<FeedMarkEntity>
    {
        public int Mark { get; set; }
        public FeedMarkEntity CreateEntity() =>
            new FeedMarkEntity
            {
                Mark = Mark,
            };
    }
}

