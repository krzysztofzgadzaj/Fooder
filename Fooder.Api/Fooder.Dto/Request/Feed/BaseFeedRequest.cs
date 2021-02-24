using System.Collections.Generic;
using Fooder.Persistence.Entities;

namespace Fooder.Dto.Request.Feed
{
    public abstract class BaseFeedRequest
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public FeedType FeedType { get; set; }
        public float MinWeight { get; set; }
        public float MaxWeight { get; set; }
        public float Price { get; set; }
        public string ProducerSiteLink { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public DogActivityLevel DogActivityLevel { get; set; }
        public IEnumerable<int> DogAfflictionsId { get; set; } = new List<int>();
    }
}
