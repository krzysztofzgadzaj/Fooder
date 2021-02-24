using System.Linq;
using Fooder.Persistence.Entities;

namespace Fooder.Dto.Request.Feed
{
    public sealed class UpdateFeedRequest : BaseFeedRequest,
        ICommandRequest<FeedEntity>
    {
        public FeedEntity CreateEntity() =>
            new FeedEntity
            {
                ShortDescription = ShortDescription,
                FullDescription = FullDescription,
                ProducerSiteLink = ProducerSiteLink,
                DogActivityLevel = DogActivityLevel,
                FeedType = FeedType,
                MaxWeight = MaxWeight,
                MinWeight = MinWeight,
                Price = Price,
                Name = Name,
                BrandNameId = BrandId,
                DogAfflictions = DogAfflictionsId.Select(
                        afflictionId => new FeedAffliction
                        {
                            AfflictionEntityId = afflictionId,
                        })
                    .ToList(),
            };
    }
}
