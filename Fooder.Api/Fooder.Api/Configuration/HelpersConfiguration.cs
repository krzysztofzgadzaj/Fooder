using Fooder.Helpers.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fooder.Api.Configuration
{
    internal static class HelpersConfiguration
    {
        private const string IdentityConfigurationSectionKey = "IdentityConfiguration";

        internal static IServiceCollection ConfigureHelpers(
            this IServiceCollection services,
            IConfiguration configuration) =>
                services.Configure<IdentityConfiguration>(configuration.GetSection(IdentityConfigurationSectionKey))
                    .AddScoped<IIdentityPort, IdentityAdapter>()
                    .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
}
