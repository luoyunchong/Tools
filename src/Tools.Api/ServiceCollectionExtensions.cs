using IGeekFan.FreeKit.Extras.CaseQuery;
using IGeekFan.FreeKit.Infrastructure.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

namespace Tools.Api
{
    public static class ServiceCollectionExtensions
    {
        #region AddCustomMVC+AddSwagger
        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseQueryStrings = true;
                options.LowercaseUrls = true;
            });

            services.AddControllers(options =>
            {
                options.ValueProviderFactories.Add(new CamelCaseValueProviderFactory());
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            })
            //.AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true)
            ;
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            //Swagger重写PascalCase，改成小写开头模式
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider,CamelCaseApiDescriptionProvider>());
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tools.Api - HTTP API",
                    Version = "v1",
                    Description = "The Tools.Api Microservice HTTP API. This is a Data-Driven/CRUD microservice sample"
                });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Tools.Api.xml"), true);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Id =  "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization", //jwt默认的参数名称
                    In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey

                });

            });

            return services;

        }
        #endregion
    }
}
