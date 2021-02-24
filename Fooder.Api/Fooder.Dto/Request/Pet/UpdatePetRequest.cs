using Fooder.Persistence.Entities;
using System.Linq;

namespace Fooder.Dto.Request.Pet
{
    public class UpdatePetRequest : BasePetRequest,
        ICommandRequest<PetEntity>
    {
        public PetEntity CreateEntity() =>
            new PetEntity
            {
                Name = Name,
                DateOfBirth = DateOfBirth,
                ActivityLevel = ActivityLevel,
                BodyWeight = BodyWeight,
                HeightInCentimetres = HeightInCentimetres,
                PetAfflictions = PetAfflictionsId.Select(
                    afflictionId => new PetAffliction
                    {
                        AfflictionEntityId = afflictionId,
                    })
                .ToList(),
            };
    }
}
