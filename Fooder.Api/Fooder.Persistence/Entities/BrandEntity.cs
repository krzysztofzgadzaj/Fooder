using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fooder.Persistence.Entities.Interface;

namespace Fooder.Persistence.Entities
{
    public sealed class BrandEntity : BaseEntity,
        IUpdatable<BrandEntity>
    {
        public BrandEntity()
        {
            Feeds = new List<FeedEntity>();
        }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        public ICollection<FeedEntity> Feeds { get; set; }
        
        public void Update(BrandEntity entity)
        {
            Name = entity.Name;
        }
    }
}
