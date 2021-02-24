using Fooder.Persistence.Entities;
using System.Collections.Generic;

namespace Fooder.Dto.ViewModel.Feed
{
    public sealed class CommentViewModel : IBuildable<CommentEntity>
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public float? CommentMark { get; private set; }
        public string AuthorName { get; set; }
        public int Level { get; private set; }
        public string UserMark { get; set; }
        public ICollection<CommentViewModel> RelatedComments { get; set; }

        public static explicit operator CommentViewModel(CommentEntity entity)
        {
            var commentViewModel = new CommentViewModel();
            commentViewModel.Construct(entity);

            return commentViewModel;
        }

        public void Construct(CommentEntity entity)
        {
            Id = entity.Id;
            Content = $"{entity.AuthorName}   -   {entity.Content}"; // ToDo: nie pytajcie o to
            CommentMark = entity.CommentMark;
            AuthorName = entity.AuthorName;
            Level = entity.RelatedCommentId.HasValue
                ? 1
                : 0;
            RelatedComments = new List<CommentViewModel>();
        }
    }
}

