using Cebc.Modules.Loans.Core.Dto;
using Cebc.Modules.Loans.Core.Entities;

namespace Cebc.Modules.Loans.Core.Mappers
{
    public interface ILoanMapper
    {
        LoanProposalDto Map(Loan loan);
    }

    public class LoanMapper : ILoanMapper
    {
        public LoanProposalDto Map(Loan loan)
        {
            return new()
            {
                AdministrationFee = loan.AdministrationFeeRounded,
                MonthlyPayment = loan.MonthlyPaymentRounded,
                TotalAmountPaid = loan.TotalAmountPaidRounded,
                TotalInterestRate = loan.TotalInterestRateRounded
            };
        }
    }
}