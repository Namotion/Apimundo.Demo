using Microsoft.Extensions.DependencyInjection;
using System;

namespace ProductService
{
    public static class OpenApiExtensions
    {
        public static IServiceCollection AddOpenApiVersionedDocument(
            this IServiceCollection services, Version version)
        {
            var assemblyBuildVersion = typeof(OpenApiExtensions).Assembly?.GetName()?.Version?.Build ?? 0;

            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "v" + version.Major;
                document.ApiGroupNames = new string[] { "v" + version.Major };
                document.Version = version.Major + "." + version.Minor;

                document.Title = "Product Service (" + version.ToString(2) + "." + assemblyBuildVersion + ")";
                document.Description = "Manages products and their metadata.";
            });

            return services;
        }
    }
}
