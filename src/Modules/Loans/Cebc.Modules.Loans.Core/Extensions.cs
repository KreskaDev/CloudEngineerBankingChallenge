using Cebc.Modules.Loans.Core.ApplicationServices;
using Cebc.Modules.Loans.Core.DomainServices;
using Cebc.Modules.Loans.Core.Mappers;
using Cebc.Modules.Loans.Core.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Cebc.Modules.Loans.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<IInstallmentCalculator, EquatedInstallmentCalculator>();
            services.AddTransient<ILoanFactory, LoanFactory>();
            services.AddTransient<ILoanIndicatorsGenerator, LoanIndicatorsGenerator>();
            services.AddTransient<ILoanMapper, LoanMapper>();
            services.AddTransient<IBankInterestProvider, BankInterestProvider>();
            services.AddTransient<ILoanInputRequirements, LoanInputRequirements>();

            return services;
        }
    }
}
