namespace Cebc.Modules.Loans.Core.Dto
{
    public class LoanProposalDto
    {
        public decimal MonthlyPayment { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public decimal AdministrationFee { get; set; }
        public decimal TotalInterestRate { get; set; }
    }
}