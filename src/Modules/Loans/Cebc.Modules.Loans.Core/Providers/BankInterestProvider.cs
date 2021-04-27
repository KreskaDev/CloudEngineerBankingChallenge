namespace Cebc.Modules.Loans.Core.Providers
{
    public interface IBankInterestProvider
    {
        decimal AnnualInterestRate { get; }
        decimal AdministrationFeePercentage { get; }
        decimal AdministrationFeeMaximumAmount { get; }
    }

    public class BankInterestProvider : IBankInterestProvider
    {
        public decimal AnnualInterestRate => 5m;
        public decimal AdministrationFeePercentage => 1m;
        public decimal AdministrationFeeMaximumAmount => 10000m;
    }
}