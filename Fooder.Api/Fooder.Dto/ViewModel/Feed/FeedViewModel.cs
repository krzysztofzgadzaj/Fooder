using System;
using System.Collections.Generic;
using Fooder.Persistence.Entities;
using System.Linq;

namespace Fooder.Dto.ViewModel.Feed
{
    public sealed class FeedViewModel : IBuildable<FeedEntity>
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public int UserMark { get; set; }
        public string BrandName { get; set; }
        public string FeedType { get; set; }
        public float Price { get; set; }
        public float AverageRating { get; set; }
        public int? RatingCount { get; set; }
        public float MinWeight { get; set; }
        public float MaxWeight { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string ProducerSiteLink { get; set; }
        public DogActivityLevel DogActivityLevel { get; set; }
        public ICollection<string> DogAfflictions { get; set; }
        public IEnumerable<int> PhotosIds { get; set; }

        public void Construct(FeedEntity entity)
        {
            Id = entity.Id;
            UniqueId = entity.UniqueId;
            Name = entity.Name;
            BrandName = entity.BrandName?.Name;
            FeedType = entity.FeedType.ToString();
            Price = entity.Price;
            ShortDescription = entity.ShortDescription;
            MinWeight = entity.MinWeight;
            MaxWeight = entity.MaxWeight;
            ShortDescription = entity.ShortDescription;
            FullDescription = entity.FullDescription;
            ProducerSiteLink = entity.ProducerSiteLink;
            DogActivityLevel = entity.DogActivityLevel;
            DogAfflictions = entity.DogAfflictions
                .Select(x => x.AfflictionEntity.Name)
                .ToList();
        }
    }
}

