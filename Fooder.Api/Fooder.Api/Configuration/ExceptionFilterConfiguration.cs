using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Fooder.Api.Configuration
{
    internal static class ExceptionFilterConfiguration
    {
        public static IServiceCollection ConfigureExceptionFilter(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcCore(ConfigureMvcFilters);
            return serviceCollection;
        }

        private static void ConfigureMvcFilters(MvcOptions config) =>
            config.Filters.Add<ExceptionFilter>();
    }
}
