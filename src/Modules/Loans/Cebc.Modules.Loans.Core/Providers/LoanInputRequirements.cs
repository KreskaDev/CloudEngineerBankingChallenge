namespace Cebc.Modules.Loans.Core.Providers
{
    public interface ILoanInputRequirements
    {
        decimal MaximumPrincipal { get; }
        decimal MaximumLoanPeriodInMonths { get; }
    }

    public class LoanInputRequirements : ILoanInputRequirements
    {
        public decimal MaximumPrincipal => 1000000;//This should be generated based on user profile
        public decimal MaximumLoanPeriodInMonths => 130 * 12; //There are no documented case of human being that lived longer.
    }
}