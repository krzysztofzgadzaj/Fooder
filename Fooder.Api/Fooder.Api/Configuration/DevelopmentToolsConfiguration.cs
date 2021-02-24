using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Fooder.Api.Configuration
{
    internal static class DevelopmentToolsConfiguration
    {
        internal static IApplicationBuilder UseDevelopmentMiddleware(
            this IApplicationBuilder applicationBuilder,
            IWebHostEnvironment webHostEnvironment)
        {
            var isDevelopment = webHostEnvironment.IsDevelopment();

            if (isDevelopment)
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }

            return applicationBuilder;
        }
    }
}
