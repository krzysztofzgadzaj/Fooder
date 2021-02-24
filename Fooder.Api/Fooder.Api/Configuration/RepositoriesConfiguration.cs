using Fooder.Repositories.Afflictions;
using Fooder.Repositories.Brand;
using Fooder.Repositories.Comment;
using Fooder.Repositories.CommentMark;
using Fooder.Repositories.Feed;
using Fooder.Repositories.FeedMark;
using Fooder.Repositories.ImageRepository;
using Fooder.Repositories.Pet;
using Fooder.Repositories.Review;
using Microsoft.Extensions.DependencyInjection;

namespace Fooder.Api.Configuration
{
    internal static class RepositoriesConfiguration
    {
        internal static IServiceCollection ConfigureRepositories(this IServiceCollection services) =>
            services.AddScoped<IFeedRepository, FeedRepository>()
                .AddScoped<ICommentRepository, CommentRepository>()
                .AddScoped<IImageRepository, ImageRepository>()
                .AddScoped<IPetRepository, PetRepository>()
                .AddScoped<IReviewRepository, ReviewRepository>()
                .AddScoped<IBrandRepository, BrandRepository>()
                .AddScoped<ICommentMarkRepository, CommentMarkRepository>()
                .AddScoped<IFeedMarkRepository, FeedMarkRepository>()
                .AddScoped<IBrandRepository, BrandRepository>()
                .AddScoped<IAfflictionRepository, AfflictionRepository>();
    }
}
