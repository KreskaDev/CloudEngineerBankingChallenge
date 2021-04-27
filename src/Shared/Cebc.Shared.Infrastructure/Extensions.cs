using System.Collections.Generic;
using System.Reflection;
using Cebc.Shared.Abstractions.Modules;
using Cebc.Shared.Abstractions.Time;
using Cebc.Shared.Infrastructure.Exceptions;
using Cebc.Shared.Infrastructure.Modules;
using Cebc.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Cebc.Shared.Infrastructure
{
    public static class Extensions
    {
        private const string CorsPolicy = "cors";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IList<Assembly> assemblies, IList<IModule> modules)
        {
            services.AddCors(cors =>
            {
                cors.AddPolicy(CorsPolicy, x =>
                {
                    x.WithOrigins("*")
                        .WithMethods("POST", "PUT", "DELETE")
                        .WithHeaders("Content-Type", "Authorization");
                });
            });
            services.AddSwaggerGen(swagger =>
            {
                swagger.CustomSchemaIds(x => x.FullName);
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cebc API",
                    Version = "v1"
                });
            });
            services.AddModuleInfo(modules);
            services.AddMemoryCache();
            services.AddErrorHandling();
            services.AddSingleton<IClock, UtcClock>();
            services.AddControllers()
                .ConfigureApplicationPartManager(manager => manager.FeatureProviders.Add(new InternalControllerFeatureProvider()));

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicy);
            app.UseErrorHandling();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.AddSwaggerOptions();
            });
            app.UseReDoc(reDoc =>
            {
                reDoc.RoutePrefix = "docs";
                reDoc.SpecUrl("/swagger/v1/swagger.json");
                reDoc.DocumentTitle = "Confab API";
            });
            app.UseRouting();

            return app;
        }

        public static void AddSwaggerOptions(this SwaggerUIOptions options)
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            options.DisplayRequestDuration();
        }
    }
}
