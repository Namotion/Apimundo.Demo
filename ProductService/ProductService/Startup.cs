using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace ProductService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers();

            services
                .AddApiVersioning(options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = false;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddVersionedApiExplorer(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            var versions = new[]
            {
                new Version(2, 1),
                new Version(1, 8)
            };

            var assemblyBuildVersion = typeof(Startup).Assembly?.GetName()?.Version?.Build ?? 0;
            foreach (var version in versions)
            {
                services.AddOpenApiDocument(options =>
                {
                    options.Title = "Product Service (" + version.ToString(2) + "." + assemblyBuildVersion + ")";
                    options.Description = "Manages products and their metadata.";

                    options.DocumentName = "v" + version.Major;
                    options.ApiGroupNames = new string[] { "v" + version.Major };
                    options.Version = version.Major + "." + version.Minor;

                    // Patch operation paths for Azure API Management
                    options.AllowReferencesWithProperties = true;
                    options.PostProcess = document =>
                    {
                        var prefix = "/api/v" + version.Major;
                        foreach (var pair in document.Paths.ToArray())
                        {
                            document.Paths.Remove(pair.Key);
                            document.Paths[pair.Key.Substring(prefix.Length)] = pair.Value;
                        }
                    };
                });
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // OpenAPI: Serve UI and specs
            app.UseSwaggerUi3();
            app.UseOpenApi(options =>
            {
                options.PostProcess = (document, request) =>
                {
                    // Patch server URL for Swagger UI
                    var prefix = "/api/v" + document.Info.Version.Split('.')[0];
                    document.Servers.First().Url += prefix;
                };
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
