using System;
using System.Linq;
using Fooder.Persistence.Entities;
using Microsoft.AspNetCore.Http;

namespace Fooder.Dto.Request.Pet
{
    public sealed class AddPetRequest : BasePetRequest,
        ICommandRequest<PetEntity>
    {
        public int OwnerId { get; set; }
        public IFormFileCollection Photos { get; set; }

        public PetEntity CreateEntity() =>
            new PetEntity
            {
                UniqueId = Guid.NewGuid(),
                Name = Name,
                DateOfBirth = DateOfBirth,
                ActivityLevel = ActivityLevel,
                OwnerId = OwnerId,
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
