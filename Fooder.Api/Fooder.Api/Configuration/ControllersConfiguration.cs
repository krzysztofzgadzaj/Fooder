using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Fooder.Api.Configuration
{
    internal static class ControllersConfiguration
    {
        internal static IServiceCollection ConfigureControllers(this IServiceCollection services) =>
            services.AddControllers()
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .Services;

        internal static IApplicationBuilder UseEndpointsMiddleware(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder.UseEndpoints(builder => builder.MapControllers());
    }
}
