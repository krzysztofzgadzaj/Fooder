using Fooder.Persistence.Entities;

namespace Fooder.Dto.Request.Feed.FeedMark
{
    public class CreateFeedMarkRequest : ICommandRequest<FeedMarkEntity>
    {
        public string UserName { get; set; }
        public int FeedId { get; set; }
        public int Mark { get; set; }

        public FeedMarkEntity CreateEntity() =>
            new FeedMarkEntity
            {
                FeedId = FeedId,
                Mark = Mark,
                UserName = UserName,
            };
    }
}

