using System;
using System.Collections.Generic;
using Fooder.Persistence.Entities;
// ReSharper disable All

namespace Fooder.Dto.Request.Pet
{
    public abstract class BasePetRequest
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DogActivityLevel ActivityLevel { get; set; }
        public float? BodyWeight { get; set; }
        public float? HeightInCentimetres { get; set; }
        public IEnumerable<int> PetAfflictionsId { get; set; } = new List<int>();
    }
}
