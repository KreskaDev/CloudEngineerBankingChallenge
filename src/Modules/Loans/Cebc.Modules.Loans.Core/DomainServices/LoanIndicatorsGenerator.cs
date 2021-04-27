using System;
using Cebc.Modules.Loans.Core.Entities;

namespace Cebc.Modules.Loans.Core.DomainServices
{
    public interface ILoanIndicatorsGenerator
    {
        LoanIndicators GenerateLoanIndicators(LoanSpecification loanSpecification, decimal administrationFee);
    }

    public class LoanIndicatorsGenerator : ILoanIndicatorsGenerator
    {
        private const decimal ErrorMargin = 0.0000001m;
        private readonly IInstallmentCalculator _installmentCalculator;

        public LoanIndicatorsGenerator(IInstallmentCalculator installmentCalculator)
        {
            _installmentCalculator = installmentCalculator;
        }

        public LoanIndicators GenerateLoanIndicators(LoanSpecification loanSpecification, decimal administrationFee)
        {
            var ear = CalculateEffectiveAnnualRate(loanSpecification);
            var apr = CalculateApr(loanSpecification, administrationFee);
            var eapr = CalculateEffectiveApr(loanSpecification, apr);

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
            var originalInstallment = _installmentCalculator.CalculateInstallment(loanSpecification);
            var borrowedAmount = loanSpecification.OriginalPrincipal - administrationFee;

            while (difference != 0)
            {
                var newOrigin = _installmentCalculator.CalculateInstallment(new LoanSpecification
                {
                    OriginalPrincipal = borrowedAmount,
                    DurationInMonths = loanSpecification.DurationInMonths,
                    CompoundFrequency = loanSpecification.CompoundFrequency,
                    AnnualInterestRate = approximation
                });

                difference = originalInstallment - newOrigin;

                if (difference is <= ErrorMargin and >= -ErrorMargin)
                {
                    break;
                }

                if (difference > 0)
                {
                    amountToAdd *= 2;
                    approximation += amountToAdd;
                }

                else
                {
                    amountToAdd /= 2;
                    approximation -= amountToAdd;
                }
            }

            return approximation;
        }

        private decimal CalculateEffectiveApr(LoanSpecification specification, decimal apr)
        {
            var periodicApr = 1 + apr / (specification.NumberOfCompoundPeriodsSimplify * 100);
            var effectiveApr = (decimal)Math.Pow((double)periodicApr, specification.NumberOfCompoundPeriodsSimplify) - 1;
            return effectiveApr * 100;
        }
    }
}