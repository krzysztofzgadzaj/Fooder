using System.ComponentModel.DataAnnotations;

namespace Fooder.Persistence.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
