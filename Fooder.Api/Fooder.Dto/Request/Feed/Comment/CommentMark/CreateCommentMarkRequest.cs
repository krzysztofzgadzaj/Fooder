using Fooder.Persistence.Entities;

namespace Fooder.Dto.Request.Feed.Comment.CommentMark
{
    public sealed class CreateCommentMarkRequest : ICommandRequest<CommentMarkEntity>
    {
        public string UserName { get; set; }
        public int CommentId { get; set; }
        public string Mark { get; set; }

        public CommentMarkEntity CreateEntity()
            => new CommentMarkEntity
            {
                UserName = UserName,
                CommentId = CommentId,
                Mark = Mark
            };
    }
}
