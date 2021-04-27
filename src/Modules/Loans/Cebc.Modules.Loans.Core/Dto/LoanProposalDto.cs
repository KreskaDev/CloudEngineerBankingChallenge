using Cebc.Modules.Loans.Core.Entities;

namespace Cebc.Modules.Loans.Core.Dto
{
    public class ProposeLoanDto
    {
        public decimal Principal { get; set; }
        public int Months { get; set; }
    }

    public class LoanProposalDto
    {
        public LoanSpecificationDto LoanSpecification { get; set; }
        public LoanIndicatorsDto LoanIndicators { get; set; }
        public LoanSummaryDto LoanSummary { get; set; }
    }

    public class LoanSpecificationDto
    {
        public CompoundFrequency CompoundFrequency { get; set; }
        public int DurationInMonths { get; set; }
        public decimal OriginalPrincipal { get; set; }
        public decimal AnnualInterestRate { get; set; }
    }

    public class LoanIndicatorsDto
    {
        public decimal EffectiveAnnualRate { get; set; }
        public decimal AnnualPercentageRate { get; set; }
        public decimal EffectiveAnnualPercentageRate { get; set; }
    }

    public class LoanSummaryDto
    {
        public decimal Installment { get; set; }
        public decimal FinanceCharge { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal AdministrationFee { get; set; }
    }
}