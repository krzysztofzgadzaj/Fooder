using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fooder.Persistence.Entities
{
    public sealed class AfflictionEntity : BaseEntity
    {
        public AfflictionEntity()
        {
            Feeds = new List<FeedAffliction>();
            Pets = new List<PetAffliction>();
        }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public ICollection<FeedAffliction> Feeds { get; set; }
        public ICollection<PetAffliction> Pets { get; set; }
    }
}
