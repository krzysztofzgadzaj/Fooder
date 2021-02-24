using Fooder.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fooder.Persistence.Contexts
{
    public class FooderContext : DbContext
    {
        public FooderContext(DbContextOptions<FooderContext> options)
            : base(options)
        {
        }

        public DbSet<FeedEntity> Feeds { get; set; }
        public DbSet<BrandEntity> Brands { get; set; }
        public DbSet<AfflictionEntity> Afflictions { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<CommentMarkEntity> CommentMarks { get; set; }
        public DbSet<FeedMarkEntity> FeedMarks { get; set; }
        public virtual DbSet<PetEntity> Pets { get; set; }
        public virtual DbSet<ImageEntity> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            CreateConstraints(modelBuilder);
            SeedInitialData(modelBuilder);
        }

        private static void CreateConstraints(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeedAffliction>()
                .HasKey(
                    s => new
                    {
                        s.FeedEntityId,
                        s.AfflictionEntityId,
                    });

            modelBuilder.Entity<PetAffliction>()
                .HasKey(
                    s => new
                    {
                        s.AfflictionEntityId,
                        s.PetEntityId,
                    });

            modelBuilder.Entity<CommentEntity>()
                .Property(p => p.CommentMark)
                .IsRequired(false);

            modelBuilder.Entity<AfflictionEntity>()
                .HasIndex(affliction => affliction.Name)
                .IsUnique();

            modelBuilder.Entity<BrandEntity>()
                .HasIndex(brand => brand.Name)
                .IsUnique();

            modelBuilder.Entity<FeedEntity>()
                .HasIndex(feed => feed.Name)
                .IsUnique();

            modelBuilder.Entity<FeedEntity>()
                .Property(feedEntity => feedEntity.DogActivityLevel)
                .HasConversion(new EnumToStringConverter<DogActivityLevel>());

            modelBuilder.Entity<FeedEntity>()
                .Property(feedEntity => feedEntity.FeedType)
                .HasConversion(new EnumToStringConverter<FeedType>());
        }

        private static void SeedInitialData(ModelBuilder modelBuilder)
        {
            var brandEntity = new BrandEntity
            {
                Id = 1,
                Name = "Brand one",
            };

            modelBuilder.Entity<BrandEntity>()
                .HasData(brandEntity);

            var afflictionEntity = new AfflictionEntity
            {
                Id = 1,
                Name = "Affliction one"
            };

            modelBuilder.Entity<AfflictionEntity>()
                .HasData(afflictionEntity);
        }
    }
}
