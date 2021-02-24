using Fooder.Api.Configuration;
using Fooder.Api.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fooder.Api
{
    public sealed class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services) =>
            services.ConfigureExceptionFilter()
                .ConfigureDatabaseConnection(_configuration)
                .ConfigureRepositories()
                .ConfigureHelpers(_configuration)
                .ConfigureServices()
                .ConfigureSwagger()
                .ConfigureCrossOriginResourceSharing(_configuration)
                .ConfigureAuthentication(_configuration, _webHostEnvironment)
                .ConfigureControllers();

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment) =>
            applicationBuilder.UseDevelopmentMiddleware(webHostEnvironment)
                .RunMigrations()
                .UseCorsMiddleware()
                .UseHttpsRedirection()
                .UseSwaggerMiddleware()
                .UseRouting()
                .UseIdentityMiddleware()
                .UseEndpointsMiddleware();
    }
}
