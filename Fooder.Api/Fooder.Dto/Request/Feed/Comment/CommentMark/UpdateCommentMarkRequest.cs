using Fooder.Persistence.Entities;

namespace Fooder.Dto.Request.Feed.Comment.CommentMark
{
    public sealed class UpdateCommentMarkRequest : ICommandRequest<CommentMarkEntity>
    {
        public string Mark { get; set; }

        public CommentMarkEntity CreateEntity() =>
            new CommentMarkEntity
            {
                Mark = Mark,
            };
    }
}

