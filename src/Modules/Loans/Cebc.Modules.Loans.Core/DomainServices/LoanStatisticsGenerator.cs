using System;
using Cebc.Modules.Loans.Core.Entities;

namespace Cebc.Modules.Loans.Core.DomainServices
{
    public interface ILoanStatisticsGenerator
    {
        LoanIndicators GenerateLoanIndicators(LoanSpecification loanSpecification, decimal administrationFee);
    }

    public class LoanStatisticsGenerator : ILoanStatisticsGenerator
    {
        private readonly IInstallmentCalculator _installmentCalculator;

        public LoanStatisticsGenerator(IInstallmentCalculator installmentCalculator)
        {
            _installmentCalculator = installmentCalculator;
        }

        public LoanIndicators GenerateLoanIndicators(LoanSpecification loanSpecification, decimal administrationFee)
        {
            var ear = CalculateEffectiveAnnualRate(loanSpecification);
            var apr = CalculateApr(loanSpecification, administrationFee);
            var eapr = CalculateEffectiveApr(apr);

            //effective continuous compound interest 
            //var continuousEffectiveAnnualRate = Math.Pow(Math.E,
            //    ((double) (annualInterestRate / 100) * months / 12));

            return new LoanIndicators
            {
                EffectiveAnnualRate = ear,
                AnnualPercentageRate = apr,
                EffectiveAnnualPercentageRate = eapr
            };
        }

        private decimal CalculateEffectiveAnnualRate(LoanSpecification loanSpecification)
        {
            var interestRate = loanSpecification.AnnualInterestRate / 100;
            var periodicRate = (double)interestRate / loanSpecification.NumberOfCompoundPeriodsSimplify;
            var effectiveAnnualRate = (decimal)(Math.Pow(1 + periodicRate, loanSpecification.NumberOfCompoundPeriodsSimplify) - 1) * 100;
            return effectiveAnnualRate;
        }

        private decimal CalculateApr(LoanSpecification loanSpecification, decimal administrationFee)
        {
            var approximation = loanSpecification.AnnualInterestRate;
            var difference = 1m;
            var amountToAdd = 0.0001m;

            var borrowedAmount = loanSpecification.OriginalPrincipal - administrationFee;

            while (difference != 0)
            {
                difference = _installmentCalculator.CalculateInstallment(loanSpecification)
                             - _installmentCalculator.CalculateInstallment(new LoanSpecification
                             {
                                 OriginalPrincipal = borrowedAmount,
                                 DurationInMonths = loanSpecification.DurationInMonths,
                                 CompoundFrequency = loanSpecification.CompoundFrequency,
                                 AnnualInterestRate = approximation
                             });

                if (difference <= 0.0000001m && difference >= -0.0000001m)
                {
                    break;
                }

                if (difference > 0)
                {
                    amountToAdd = amountToAdd * 2;
                    approximation = approximation + amountToAdd;
                }

                else
                {
                    amountToAdd = amountToAdd / 2;
                    approximation = approximation - amountToAdd;
                }
            }

            return approximation;
        }

        private decimal CalculateEffectiveApr(decimal apr)
        {
            var periodicApr = 1 + apr / (12 * 100);
            var effectiveApr = (decimal)Math.Pow((double)periodicApr, 12) - 1;

            return effectiveApr * 100;
        }
    }
}