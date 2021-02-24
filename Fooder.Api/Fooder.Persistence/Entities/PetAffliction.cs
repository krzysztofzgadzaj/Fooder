namespace Fooder.Persistence.Entities
{
    public sealed class PetAffliction
    {
        public int PetEntityId { get; set; }
        public PetEntity PetEntity { get; set; }
        public int AfflictionEntityId { get; set; }
        public AfflictionEntity AfflictionEntity { get; set; }
    }
}
