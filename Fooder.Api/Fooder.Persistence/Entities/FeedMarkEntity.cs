using Fooder.Persistence.Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fooder.Persistence.Entities
{
    public sealed class FeedMarkEntity : BaseEntity,
        IUpdatable<FeedMarkEntity>
    {
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string UserName { get; set; }
        [Required]
        public int FeedId { get; set; }
        [Required]
        public int Mark { get; set; }

        public void Update(FeedMarkEntity entity)
        {
            UserName = entity.UserName;
        }
    }
}
