using Fooder.Persistence.Entities.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fooder.Persistence.Entities
{
    public sealed class CommentEntity : BaseEntity, IUpdatable<CommentEntity>
    {
        [Required]
        [Column(TypeName = "nvarchar(1000)")]
        public string Content { get; set; }
        public float? CommentMark { get; set; }
        public string AuthorName { get; set; }
        public int? RelatedCommentId { get; set; }
        public CommentEntity RelatedComment { get; set; }
        public ICollection<CommentEntity> RelatedComments { get; set; }
        [Required]
        public int FeedEntityId { get; set; }
        public FeedEntity FeedEntity { get; set; }

        public void Update(CommentEntity entity)
        {
            Content = entity.Content;
            CommentMark = entity.CommentMark;
        }
    }
}
