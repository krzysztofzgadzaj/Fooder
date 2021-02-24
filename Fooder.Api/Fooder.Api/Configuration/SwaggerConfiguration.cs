using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Fooder.Api.Configuration
{
    internal static class SwaggerConfiguration
    {
        private const string Name = "Fooder Api";
        private const string VersionString = "v1";
        private const string SwaggerUrl = "/swagger/v1/swagger.json";
        private const string AuthorizationHeaderKey = "Authorization";
        private const string AuthorizationHeaderDescription = "JWT Authorization header used in the bearer scheme";
        private const string DocumentDescription = "Fooder Swagger Surface";
        private const string Author = "Grupa 1, sekcja 2";
        private const string AuthorMailAddress = "alekste778@student.polsl.pl";
        private const string BearerAuthenticationName = "Bearer";

        internal static IServiceCollection ConfigureSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(
                        VersionString,
                        new OpenApiInfo
                        {
                            Title = Name,
                            Version = VersionString,
                            Description = DocumentDescription,
                            Contact = new OpenApiContact
                            {
                                Name = Author,
                                Email = AuthorMailAddress,
                            },
                        });

                    options.AddSecurityDefinition(
                        BearerAuthenticationName,
                        new OpenApiSecurityScheme
                        {
                            Description = AuthorizationHeaderDescription,
                            Name = AuthorizationHeaderKey,
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = BearerAuthenticationName,
                        });

                    options.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = BearerAuthenticationName,
                                    },
                                },
                                Array.Empty<string>()
                            },
                        });

                    options.CustomSchemaIds(type => type.FullName);
                });

        internal static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder.UseSwagger()
                .UseSwaggerUI(
                    options =>
                    {
                        options.SwaggerEndpoint(SwaggerUrl, Name);
                        options.RoutePrefix = string.Empty;
                    });
    }
}
