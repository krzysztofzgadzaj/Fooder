using System;
using System.Collections.Generic;
using System.Linq;
using Fooder.Persistence.Entities;

namespace Fooder.Dto.ViewModel
{
    public sealed class PetViewModel : BaseViewModel, IBuildable<PetEntity>
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ActivityLevel { get; set; }
        public int OwnerId { get; set; }
        public float? BodyWeight { get; set; }
        public float? HeightInCentimetres { get; set; }
        public IEnumerable<int> PhotosIds { get; set; }
        public ICollection<string> PetAfflictions { get; set; }

        public void Construct(PetEntity petEntity)
        {
            Id = petEntity.Id;
            UniqueId = petEntity.UniqueId;
            Name = petEntity.Name;
            DateOfBirth = petEntity.DateOfBirth;
            ActivityLevel = Enum.GetName(typeof(DogActivityLevel), petEntity.ActivityLevel);
            OwnerId = petEntity.OwnerId;
            BodyWeight = petEntity.BodyWeight;
            HeightInCentimetres = petEntity.HeightInCentimetres;
            PetAfflictions = petEntity.PetAfflictions
                .Select(x => x.AfflictionEntity.Name)
                .ToList();
        }
    }
}
