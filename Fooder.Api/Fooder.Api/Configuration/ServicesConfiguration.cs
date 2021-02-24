using Fooder.Services.Affliction;
using Fooder.Services.Brand;
using Fooder.Services.Comment;
using Fooder.Services.Feed;
using Fooder.Services.Image;
using Fooder.Services.Pet;
using Fooder.Services.Review;
using Fooder.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace Fooder.Api.Configuration
{
    internal static class ServicesConfiguration
    {
        internal static IServiceCollection ConfigureServices(this IServiceCollection services) =>
            services.AddScoped<IUserService, UserService>()
                .AddScoped<IFeedService, FeedService>()
                .AddScoped<IImageService, ImageService>()
                .AddScoped<IPetService, PetService>()
                .AddScoped<ICommentService, CommentService>()
                .AddScoped<IReviewService, ReviewService>()
                .AddScoped<IBrandService, BrandService>()
                .AddScoped<IAfflictionService, AfflictionService>();
    }
}
