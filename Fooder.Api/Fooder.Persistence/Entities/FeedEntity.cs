using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fooder.Persistence.Entities.Interface;

namespace Fooder.Persistence.Entities
{
    public sealed class FeedEntity : BaseEntity,
        IUpdatable<FeedEntity>
    {
        public FeedEntity()
        {
            DogAfflictions = new List<FeedAffliction>();
            Reviews = new List<ReviewEntity>();
            Comments = new List<CommentEntity>();
        }

        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string Name { get; set; }

        [Required] public string ShortDescription { get; set; }

        [Required] public string FullDescription { get; set; }

        public Guid UniqueId { get; set; }

        [Required] public int BrandNameId { get; set; }

        [Required] public BrandEntity BrandName { get; set; }

        [Required] public FeedType FeedType { get; set; }

        [Required] public float MinWeight { get; set; }

        [Required] public float MaxWeight { get; set; }

        [Required] public float Price { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string ProducerSiteLink { get; set; }

        [Required] public DogActivityLevel DogActivityLevel { get; set; }

        [Required] public ICollection<FeedAffliction> DogAfflictions { get; set; }

        public ICollection<ReviewEntity> Reviews { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }

        public void Update(FeedEntity entity)
        {
            ShortDescription = entity.ShortDescription;
            FullDescription = entity.FullDescription;
            ProducerSiteLink = entity.ProducerSiteLink;
            DogActivityLevel = entity.DogActivityLevel;
            FeedType = entity.FeedType;
            MaxWeight = entity.MaxWeight;
            MinWeight = entity.MinWeight;
            Price = entity.Price;
            Name = entity.Name;
            BrandNameId = entity.BrandNameId;
            DogAfflictions = entity.DogAfflictions;
        }
    }
}
