using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

namespace Tools.Api
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration c)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tools.Api - HTTP API",
                    Version = "v1",
                    Description = "The Tools.Api Microservice HTTP API. This is a Data-Driven/CRUD microservice sample"
                });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Tools.Api.xml"), true);
            });

            return services;
        }
    }
}
