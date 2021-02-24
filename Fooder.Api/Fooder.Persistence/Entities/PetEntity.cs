using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fooder.Constants;
using Fooder.Persistence.Entities.Interface;

namespace Fooder.Persistence.Entities
{
    public sealed class PetEntity : BaseEntity, IUpdatable<PetEntity>
    {
        public Guid UniqueId { get; set; }
        [Required, Column(TypeName = ColumnTypes.NVarChar100),]
        public string Name { get; set; }
        [Column(TypeName = ColumnTypes.DateTime)]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public DogActivityLevel ActivityLevel { get; set; }
        [Required]
        public int OwnerId { get; set; }
        public float? BodyWeight { get; set; }
        public float? HeightInCentimetres { get; set; }
        [Required]
        public ICollection<PetAffliction> PetAfflictions { get; set; } = new List<PetAffliction>();

        public void Update(PetEntity entity)
        {
            Name = entity.Name;
            DateOfBirth = entity.DateOfBirth;
            ActivityLevel = entity.ActivityLevel;
            BodyWeight = entity.BodyWeight;
            HeightInCentimetres = entity.HeightInCentimetres;
        }
    }
}

