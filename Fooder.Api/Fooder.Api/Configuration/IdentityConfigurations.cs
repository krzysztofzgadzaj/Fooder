using System;
using System.Text;
using Fooder.Api.Configuration.Model;
using Fooder.Api.Utils.AuthorizationPolicies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Fooder.Api.Configuration
{
    internal static class IdentityConfigurations
    {
        private const string TokenManagementSectionKey = "TokenSettings";

        internal static IServiceCollection ConfigureAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            var tokenManagementSection = configuration.GetSection(TokenManagementSectionKey)
                .Get<TokenSettings>();

            var secretKey = Encoding.ASCII.GetBytes(tokenManagementSection.SecretKey);

            var isProduction = hostingEnvironment.IsProduction();

            return services.AddAuthentication(
                    o =>
                    {
                        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(
                    o =>
                    {
                        o.RequireHttpsMetadata = isProduction;
                        o.SaveToken = true;
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                        };
                    })
                .Services
                .AddAuthorization()
                .Configure<TokenSettings>(configuration.GetSection(TokenManagementSectionKey));
        }

        internal static IApplicationBuilder UseIdentityMiddleware(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder.UseAuthentication()
                .UseAuthorization();

        private static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            services
                .AddAuthorization(options =>
                {
                    options
                        .AddPolicy(nameof(FeedAdditionPermittedPolicy),
                            policy =>
                                policy
                                    .Requirements
                                    .Add(new FeedAdditionPermittedPolicy(serviceProvider)));

                    options
                        .AddPolicy(nameof(BrandAdditionPermittedPolicy),
                            policy =>
                                policy
                                    .Requirements
                                    .Add(new BrandAdditionPermittedPolicy(serviceProvider)));

                    options.AddPolicy(
                        nameof(ReviewAdditionPermittedPolicy),
                        policy => policy
                            .Requirements
                            .Add(new ReviewAdditionPermittedPolicy(serviceProvider)));
                });

            return services;
        }
    }
}
