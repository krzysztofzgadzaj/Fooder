using Fooder.Persistence.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fooder.Persistence.Entities
{
    public sealed class ReviewEntity : BaseEntity, IUpdatable<ReviewEntity>
    {
        [Required]
        [Column(TypeName = "nvarchar(1000)")]
        public string Content { get; set; }
        
        [Required]
        public int FeedEntityId { get; set; }
        
        [Required]
        public int AuthorId { get; set; }

        public void Update(ReviewEntity entity)
        {
            Content = entity.Content;
        }
    }
}
