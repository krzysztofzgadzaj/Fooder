namespace Fooder.Persistence.Entities
{
    public sealed class FeedAffliction
    {
        public int FeedEntityId { get; set; }
        public FeedEntity FeedEntity { get; set; }
        public int AfflictionEntityId { get; set; }
        public AfflictionEntity AfflictionEntity { get; set; }
    }
}
