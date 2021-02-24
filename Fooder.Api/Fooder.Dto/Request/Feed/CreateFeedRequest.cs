using System;
using System.Linq;
using Fooder.Persistence.Entities;
using Microsoft.AspNetCore.Http;

namespace Fooder.Dto.Request.Feed
{
    public sealed class CreateFeedRequest : BaseFeedRequest,
        ICommandRequest<FeedEntity>
    {
        public IFormFileCollection Photos { get; set; }

        public FeedEntity CreateEntity() =>
            new FeedEntity
            {
                UniqueId = Guid.NewGuid(),
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
