using Fooder.Persistence.Entities;

namespace Fooder.Dto.Request.Feed.Comment
{
    public sealed class UpdateCommentRequest : ICommandRequest<CommentEntity>
    {
        public string Content { get; set; }
        public float? ProductMark { get; set; }
        public float? CommentMark { get; set; }

        public CommentEntity CreateEntity() =>
            new CommentEntity
            {
                Content = Content,
                CommentMark = CommentMark,
            };
    }
}

