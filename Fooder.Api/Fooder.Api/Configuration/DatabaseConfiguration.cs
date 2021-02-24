using System;
using Fooder.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fooder.Api.Configuration
{
    internal static class DatabaseConfiguration
    {
        private const string FooderContextConnectionStringKey = "Fooder";
        private const string FooderContextMigrationAssemblyName = "Fooder.Persistence";

        internal static IServiceCollection ConfigureDatabaseConnection(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services.ConfigureDatabaseContext<FooderContext>(
                configuration,
                FooderContextConnectionStringKey,
                FooderContextMigrationAssemblyName);

        private static IServiceCollection ConfigureDatabaseContext<T>(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringSectionName,
            string migrationAssemblyName = null) where T : DbContext
        {
            var connectionString = configuration.GetConnectionString(connectionStringSectionName);

            var databaseContextOptionsAction = migrationAssemblyName != null
                ? (Action<DbContextOptionsBuilder>) (dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer(
                    connectionString,
                    sqlServerDbContextOptionsBuilder =>
                        sqlServerDbContextOptionsBuilder.MigrationsAssembly(migrationAssemblyName)))
                : dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer(connectionString);

            return services.AddDbContext<T>(databaseContextOptionsAction);
        }
    }
}
