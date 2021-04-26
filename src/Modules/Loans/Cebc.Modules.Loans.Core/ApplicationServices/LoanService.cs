using Cebc.Modules.Loans.Core.DomainServices;
using Cebc.Modules.Loans.Core.Dto;
using Cebc.Modules.Loans.Core.Exceptions;
using Cebc.Modules.Loans.Core.Mappers;
using Cebc.Modules.Loans.Core.Providers;

namespace Cebc.Modules.Loans.Core.ApplicationServices
{
    public interface ILoanService
    {
        LoanProposalDto ProposeLoan(decimal principal, int months);
    }

    public class LoanService : ILoanService
    {
        private readonly ILoanGenerator _loanGenerator;
        private readonly ILoanMapper _loanMapper;
        private readonly ILoanInputRequirements _loanInputRequirements;

        public LoanService(
            ILoanGenerator loanGenerator, 
            ILoanMapper loanMapper, 
            ILoanInputRequirements loanInputRequirements)
        {
            _loanGenerator = loanGenerator;
            _loanMapper = loanMapper;
            _loanInputRequirements = loanInputRequirements;
        }

        public LoanProposalDto ProposeLoan(decimal principal, int months)
        {
            if (principal <= 0 || principal > _loanInputRequirements.MaximumPrincipal)
            {
                throw new LoanException("principal amount is incorrect");
            }

            if (months <= 0 || months > _loanInputRequirements.MaximumLoanPeriodInMonths)
            {
                throw new LoanException("loan period is incorrect");
            }

            var loan = _loanGenerator.GenerateLoan(principal, months);
            return _loanMapper.Map(loan);
        }
    }
}
