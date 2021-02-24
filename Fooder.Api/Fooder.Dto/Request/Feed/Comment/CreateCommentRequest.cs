using Fooder.Persistence.Entities;

namespace Fooder.Dto.Request.Feed.Comment
{
    public sealed class CreateCommentRequest : ICommandRequest<CommentEntity>
    {
        public string Content { get; set; }
        public int? ParentCommentId { get; set; }
        public int FeedId { get; set; }
        public string AuthorName { get; set; }

        public CommentEntity CreateEntity() =>
            new CommentEntity
            {
                Content = Content,
                FeedEntityId = FeedId,
                RelatedCommentId = ParentCommentId,
                AuthorName = AuthorName,
                CommentMark = 0,
            };
    }
}

