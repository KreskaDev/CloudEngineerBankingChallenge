using System.Collections.Generic;
using System.Reflection;
using Cebc.Shared.Abstractions.Modules;
using Cebc.Shared.Infrastructure;
using Cebc.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cebc.Bootstrapper
{
    public class Startup
    {
        private readonly IList<Assembly> _assemblies;
        private readonly IList<IModule> _modules;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _assemblies = ModuleLoader.LoadAssemblies();
            _modules = ModuleLoader.LoadModules(_assemblies);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(_assemblies, _modules);
            foreach (var module in _modules)
            {
                module.Register(services);
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseInfrastructure();
            foreach (var module in _modules)
            {
                module.Use(app);
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Cebc API!"));
                endpoints.MapModuleInfo();
            });

            _assemblies.Clear();
            _modules.Clear();
        }
    }
}
