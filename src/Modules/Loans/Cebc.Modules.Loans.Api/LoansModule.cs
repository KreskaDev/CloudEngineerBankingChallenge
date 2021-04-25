using Cebc.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cebc.Modules.Loans.Api
{
    internal class LoansModule : IModule
    {
        public const string BasePath = "loans-module";
        public string Name => "Loans";
        public string Path => BasePath;

        public void Register(IServiceCollection services)
        {
            
        }

        public void Use(IApplicationBuilder app)
        {
            
        }
    }
}
