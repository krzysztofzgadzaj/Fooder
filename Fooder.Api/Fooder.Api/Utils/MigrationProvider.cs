using Fooder.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fooder.Api.Utils
{
    internal static class MigrationProvider
    {
        internal static IApplicationBuilder RunMigrations(
            this IApplicationBuilder applicationBuilder) =>
                applicationBuilder
                    .RunContextMigrations<FooderContext>();

        private static IApplicationBuilder RunContextMigrations<T>(this IApplicationBuilder applicationBuilder) where T : DbContext
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var desiredContext = serviceScope.ServiceProvider.GetService<T>();

            desiredContext.Database.Migrate();

            return applicationBuilder;
        }
    }
}
