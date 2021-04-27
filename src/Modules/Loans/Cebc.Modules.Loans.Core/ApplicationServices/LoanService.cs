using System;
using Cebc.Modules.Loans.Core.DomainServices;
using Cebc.Modules.Loans.Core.Dto;
using Cebc.Modules.Loans.Core.Entities;
using Cebc.Modules.Loans.Core.Exceptions;
using Cebc.Modules.Loans.Core.Mappers;
using Cebc.Modules.Loans.Core.Providers;

namespace Cebc.Modules.Loans.Core.ApplicationServices
{
    public interface ILoanService
    {
        LoanProposalDto ProposeLoan(ProposeLoanDto dto);
    }

    public class LoanService : ILoanService
    {
        private readonly ILoanFactory _loanFactory;
        private readonly ILoanMapper _loanMapper;
        private readonly ILoanInputRequirements _loanInputRequirements;
        private readonly IBankInterestProvider _bankInterestProvider;

        public LoanService(
            ILoanFactory loanFactory, 
            ILoanMapper loanMapper, 
            ILoanInputRequirements loanInputRequirements, 
            IBankInterestProvider bankInterestProvider)
        {
            _loanFactory = loanFactory;
            _loanMapper = loanMapper;
            _loanInputRequirements = loanInputRequirements;
            _bankInterestProvider = bankInterestProvider;
        }

        public LoanProposalDto ProposeLoan(ProposeLoanDto dto)
        {
            if (dto.Principal <= 0 || dto.Principal > _loanInputRequirements.CustomerMaximumPrincipal)
            {
                throw new LoanException("principal amount is incorrect");
            }

            if (dto.Months <= 0 || dto.Months > _loanInputRequirements.MaximumLoanPeriodInMonths)
            {
                throw new LoanException("loan period is incorrect");
            }

            var loanSpecification = new LoanSpecification
            {
                OriginalPrincipal = dto.Principal,
                DurationInMonths = dto.Months,
                CompoundFrequency = CompoundFrequency.Monthly,
                AnnualInterestRate = _bankInterestProvider.AnnualInterestRate
            };

            var loan = _loanFactory.CreateLoan(loanSpecification);

            return _loanMapper.Map(loan);
        }
    }
}
