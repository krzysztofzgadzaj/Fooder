using Fooder.Persistence.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fooder.Persistence.Entities
{
    public class CommentMarkEntity : BaseEntity,
        IUpdatable<CommentMarkEntity>
    {
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string UserName { get; set; }
        [Required]
        public int CommentId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(15)")]
        public string Mark { get; set; }

        public void Update(CommentMarkEntity entity)
        {
            Mark = entity.Mark;
        }
    }
}
