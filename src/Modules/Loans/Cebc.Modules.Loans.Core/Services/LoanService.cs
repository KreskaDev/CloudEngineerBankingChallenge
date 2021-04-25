using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cebc.Modules.Loans.Core.Dto;
using Cebc.Modules.Loans.Core.Entities;
using Cebc.Modules.Loans.Core.Policy;

namespace Cebc.Modules.Loans.Core.Services
{
    public interface ILoanService
    {
        LoanProposalDto ProposeLoan(decimal principal, int months);
    }

    public class LoanService : ILoanService
    {
        private readonly ILoanGenerator _loanGenerator;
        private readonly ILoanMapper _loanMapper;

        public LoanService(
            ILoanGenerator loanGenerator, 
            ILoanMapper loanMapper)
        {
            _loanGenerator = loanGenerator;
            _loanMapper = loanMapper;
        }

        public LoanProposalDto ProposeLoan(decimal principal, int months) 
            => _loanMapper.Map(_loanGenerator.GenerateLoan(principal, months));
    }

    public interface ILoanMapper
    {
        LoanProposalDto Map(Loan loan);
    }

    public class LoanMapper : ILoanMapper
    {
        public LoanProposalDto Map(Loan loan)
        {
            return new LoanProposalDto
            {
                AdministrationFee = loan.AdministrationFeeRounded,
                MonthlyPayment = loan.MonthlyPaymentRounded,
                TotalAmountPaid = loan.TotalAmountPaidRounded,
                TotalInterestRate = loan.TotalInterestRateRounded
            };
        }
    }
}
