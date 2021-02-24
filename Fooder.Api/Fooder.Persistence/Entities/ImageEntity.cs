using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fooder.Constants;
using Fooder.Persistence.Entities.Interface;

namespace Fooder.Persistence.Entities
{
    public sealed class ImageEntity : BaseEntity, IUpdatable<ImageEntity>
    {
        [Required, Column(TypeName = ColumnTypes.NVarChar100),]
        public string Name { get; set; }
        [Required, Column(TypeName = ColumnTypes.VarBinaryMax),]
        public byte[] Content { get; set; }
        [Required, Column(TypeName = ColumnTypes.NVarChar100),]
        public string ContentType { get; set; }
        [Required]
        public Guid OwnerId { get; set; }

        public void Update(ImageEntity entity) =>
            throw new NotImplementedException();
    }
}
