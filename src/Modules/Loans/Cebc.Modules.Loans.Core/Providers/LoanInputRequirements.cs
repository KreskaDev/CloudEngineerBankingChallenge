namespace Cebc.Modules.Loans.Core.Providers
{
    public interface ILoanInputRequirements
    {
        decimal CustomerMaximumPrincipal { get; }
        decimal MaximumLoanPeriodInMonths { get; }
    }

    public class LoanInputRequirements : ILoanInputRequirements
    {
        /// <summary>
        /// This should be generated based on user profile and his economic situation
        /// Other bounded context should provide this value.
        /// </summary>
        public decimal CustomerMaximumPrincipal => 1000000;

        /// <summary>
        /// This should be generated based on user profile and his economic situation
        /// There are no documented case of human being that lived longer.
        /// </summary>
        public decimal MaximumLoanPeriodInMonths => 130 * 12; 
    }
}