using System;
using Cebc.Modules.Loans.Core.Entities;

namespace Cebc.Modules.Loans.Core.DomainServices
{
    public interface IInstallmentCalculator
    {
        decimal CalculateInstallment(LoanSpecification loanSpecification);
    }

    public class EquatedInstallmentCalculator : IInstallmentCalculator
    {
        public decimal CalculateInstallment(LoanSpecification loanSpecification)
        {
            var interestRate = loanSpecification.AnnualInterestRate / 100;
            var periodicRate = (double)interestRate / loanSpecification.NumberOfCompoundPeriodsSimplify;
            var qPowN = Math.Pow(1 + periodicRate, loanSpecification.DurationInMonths);
            var installment = (decimal)((double)loanSpecification.OriginalPrincipal * qPowN * (periodicRate / (qPowN - 1)));

            return installment;
        }
    }
}