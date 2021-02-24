using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fooder.Api.Configuration
{
    internal static class CrossOriginResourceSharingConfiguration
    {
        private const string AllowedOriginsSectionName = "AllowedOrigins";
        private const string PolicyName = "FooderPolicy";

        internal static IServiceCollection ConfigureCrossOriginResourceSharing(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var originsContainer = configuration.GetSection(AllowedOriginsSectionName)
                .Get<ICollection<string>>();

            return services.AddCors(
                o =>
                {
                    o.AddPolicy(
                        PolicyName,
                        b =>
                        {
                            foreach (var origin in originsContainer)
                            {
                                b.WithOrigins(origin)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials();
                            }
                        });
                });
        }

        internal static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder.UseCors(PolicyName);
    }
}
