using Cebc.Modules.Loans.Core.ApplicationServices;
using Cebc.Modules.Loans.Core.DomainServices;
using Cebc.Modules.Loans.Core.Dto;
using Cebc.Modules.Loans.Core.Entities;
using Cebc.Modules.Loans.Core.Mappers;
using Cebc.Modules.Loans.Core.Providers;
using FluentAssertions;
using Xunit;

namespace Cebc.Modules.Loans.Tests
{
    public class AcceptanceTests
    {
        private LoanProposalDto Execute(decimal amount, int months)
        {
            //var bankInterestRateRepository = Substitute.For<IBankInterestProvider>();

            var loanService = new LoanService(
                new LoanFactory(
                    new EquatedInstallmentCalculator(),
                    new LoanIndicatorsGenerator(new EquatedInstallmentCalculator()),
                    new BankInterestProvider()
                ),
                new LoanMapper(),
                new LoanInputRequirements(),
                new BankInterestProvider()
            );

            return loanService.ProposeLoan(new ProposeLoanDto { Principal = amount, Months = months });
        }

        [Fact]
        public void ProvidedExample_Success()
        {
            var loanAmount = 500000;
            var period = 10 * 12;

            var loanResult = Execute(loanAmount, period);

            loanResult.LoanSummary.Installment.Should().Be(5303.28m);
            loanResult.LoanSummary.TotalInterest.Should().Be(136393.09m);
            loanResult.LoanSummary.AdministrationFee.Should().Be(5000.00m);
            loanResult.LoanSummary.TotalAmountPaid.Should().Be(641393.09m);
            loanResult.LoanSummary.FinanceCharge.Should().Be(141393.09m);

            loanResult.LoanIndicators.EffectiveAnnualRate.Should().Be(5.116m);
            loanResult.LoanIndicators.AnnualPercentageRate.Should().Be(5.219m);
            loanResult.LoanIndicators.EffectiveAnnualPercentageRate.Should().Be(5.345m);

            loanResult.LoanSpecification.OriginalPrincipal.Should().Be(500000M);
            loanResult.LoanSpecification.DurationInMonths.Should().Be(120);
            loanResult.LoanSpecification.AnnualInterestRate.Should().Be(5);
            loanResult.LoanSpecification.CompoundFrequency.Should().Be(CompoundFrequency.Monthly);
        }
    }
}
