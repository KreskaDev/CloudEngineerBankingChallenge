using System;
using Cebc.Modules.Loans.Core.DomainServices;
using Cebc.Modules.Loans.Core.Entities;
using Cebc.Modules.Loans.Core.Providers;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Cebc.Modules.Loans.Tests
{
    public class EquatedMonthlyInstallmentGeneratorTests
    {
        private Loan Execute(decimal amount, int months)
        {
            //var bankInterestRateRepository = Substitute.For<IBankInterestProvider>();

            var generator = new LoanFactory(new EquatedInstallmentCalculator(), new BankInterestProvider());

            var loan = generator.CreateLoan(new LoanSpecification
            {
                OriginalPrincipal = amount,
                DurationInMonths = months,
                AnnualInterestRate = 5.0m,
                CompoundFrequency = CompoundFrequency.Monthly
            });

            var gen2 = new LoanStatisticsGenerator(new EquatedInstallmentCalculator());
            var statistics = gen2.GenerateLoanIndicators(loan);



            return loan;
        }

        [Fact]
        public void ProvidedExample_Success()
        {
            var loanAmount = 500000;
            var period = 10 * 12;

            var loanResult = Execute(loanAmount, period);

            loanResult.MonthlyPaymentRounded.Should().Be(5303.28m);
            loanResult.TotalInterestRateRounded.Should().Be(136393.09m);
            loanResult.AdministrationFeeRounded.Should().Be(5000.00m);

            loanResult.TotalAmountPaidRounded.Should().Be(641393.09m);
        }
    }
}
