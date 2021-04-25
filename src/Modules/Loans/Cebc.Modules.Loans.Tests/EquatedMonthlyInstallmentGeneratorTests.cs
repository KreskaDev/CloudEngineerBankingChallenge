using System;
using Cebc.Modules.Loans.Core.Entities;
using Cebc.Modules.Loans.Core.Policy;
using Cebc.Modules.Loans.Core.Services;
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

            var generator = new EquatedMonthlyInstallmentGenerator(new BankInterestRate());

            return generator.GenerateLoan(amount, months);
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

            //loanResult.TotalAmountPaidRounded.Should().Be(636393.6m);
        }
    }
}
