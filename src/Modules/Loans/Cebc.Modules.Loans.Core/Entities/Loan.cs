using System;

namespace Cebc.Modules.Loans.Core.Entities
{
    public class Loan
    {
        public Loan(LoanSpecification specification, decimal installment, decimal administrationFee, LoanIndicators indicators)
        {
            Specification = specification;
            Installment = installment;
            AdministrationFee = administrationFee;
            Indicators = indicators;
        }

        public LoanSpecification Specification { get; set; }
        public LoanIndicators Indicators { get; set; }

        public decimal Installment { get; }
        public decimal AdministrationFee { get; }
        public decimal TotalInterest => Installment * Specification.DurationInMonths - Specification.OriginalPrincipal;
        public decimal FinanceCharge => TotalInterest + AdministrationFee;
        public decimal TotalAmountPaid => Specification.OriginalPrincipal + FinanceCharge;
    }

    public record LoanSpecification
    {
        public CompoundFrequency CompoundFrequency { get; set; }
        public int DurationInMonths { get; set; }
        public decimal OriginalPrincipal { get; set; }
        public decimal AnnualInterestRate { get; set; }

        public int NumberOfCompoundPeriodsSimplify
            => CompoundFrequency switch
            {
                CompoundFrequency.Daily => 365,
                CompoundFrequency.Weekly => 52,
                CompoundFrequency.Monthly => 12,
                CompoundFrequency.Quarterly => 4,
                CompoundFrequency.Annually => 1,
                _ => throw new ArgumentOutOfRangeException(
                    nameof(CompoundFrequency), "Undefined compound frequency")
            };

        
    }

    public record LoanIndicators
    {
        public decimal EffectiveAnnualRate { get; set; }
        public decimal AnnualPercentageRate { get; set; }
        public decimal EffectiveAnnualPercentageRate { get; set; }
    }

    public enum CompoundFrequency
    {
        Daily,
        Weekly,
        Monthly,
        Quarterly,
        Annually
    }
}
