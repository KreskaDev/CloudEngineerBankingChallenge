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
        LoanProposalDto ProposeLoan(decimal principal, int months);
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

        public LoanProposalDto ProposeLoan(decimal principal, int months)
        {
            if (principal <= 0 || principal > _loanInputRequirements.CustomerMaximumPrincipal)
            {
                throw new LoanException("principal amount is incorrect");
            }

            if (months <= 0 || months > _loanInputRequirements.MaximumLoanPeriodInMonths)
            {
                throw new LoanException("loan period is incorrect");
            }

            var loanSpecification = new LoanSpecification
            {
                OriginalPrincipal = principal,
                DurationInMonths = months,
                CompoundFrequency = CompoundFrequency.Monthly,
                AnnualInterestRate = _bankInterestProvider.AnnualInterestRate
            };

            var loan = _loanFactory.CreateLoan(loanSpecification);

            return _loanMapper.Map(loan);
        }
    }
}
