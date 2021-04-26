namespace Cebc.Modules.Loans.Core.Providers
{
    public interface IBankInterestProvider
    {
        decimal InterestRate { get; }
        decimal InterestRatePeriod { get; }
        decimal AdministrationFeePercentage { get; }
        decimal AdministrationFeeMaximumAmount { get; }
    }

    public class BankInterestRate : IBankInterestProvider
    {
        public decimal InterestRate => 5m;
        public decimal InterestRatePeriod => 12;
        public decimal AdministrationFeePercentage => 1m;
        public decimal AdministrationFeeMaximumAmount => 10000m;
    }
}