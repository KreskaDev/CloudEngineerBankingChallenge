using Cebc.Modules.Loans.Core.Entities;
using Cebc.Modules.Loans.Core.Providers;

namespace Cebc.Modules.Loans.Core.DomainServices
{
    public interface ILoanFactory
    {
        Loan CreateLoan(LoanSpecification loanSpecification);
    }

    public class LoanFactory : ILoanFactory
    {
        private readonly IInstallmentCalculator _installmentCalculator;
        private readonly ILoanIndicatorsGenerator _loanIndicatorsGenerator;
        private readonly IBankInterestProvider _bankInterestProvider;

        public LoanFactory(
            IInstallmentCalculator installmentCalculator, 
            ILoanIndicatorsGenerator loanIndicatorsGenerator, 
            IBankInterestProvider bankInterestProvider)
        {
            _installmentCalculator = installmentCalculator;
            _loanIndicatorsGenerator = loanIndicatorsGenerator;
            _bankInterestProvider = bankInterestProvider;
        }

        public Loan CreateLoan(LoanSpecification loanSpecification)
        {
            var administrationFee = CalculateAdministrationFee(loanSpecification);
            var installment = _installmentCalculator.CalculateInstallment(loanSpecification);
            var indicators = _loanIndicatorsGenerator.GenerateLoanIndicators(loanSpecification, administrationFee);

            return new Loan(loanSpecification, installment, administrationFee, indicators);
        }

        private decimal CalculateAdministrationFee(LoanSpecification loanSpecification)
        {
            var percentageAmount = loanSpecification.OriginalPrincipal / 100 * _bankInterestProvider.AdministrationFeePercentage;
            return percentageAmount < _bankInterestProvider.AdministrationFeeMaximumAmount
                ? percentageAmount
                : _bankInterestProvider.AdministrationFeeMaximumAmount;
        }
    }
}