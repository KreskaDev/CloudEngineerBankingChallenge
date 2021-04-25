using System;
using Cebc.Modules.Loans.Core.Entities;

namespace Cebc.Modules.Loans.Core.Policy
{
    public class EquatedMonthlyInstallmentGenerator : ILoanGenerator
    {
        private readonly IBankInterestProvider _bankInterestProvider;

        public EquatedMonthlyInstallmentGenerator(IBankInterestProvider bankInterestProvider)
        {
            _bankInterestProvider = bankInterestProvider;
        }

        public Loan GenerateLoan(decimal principalAmount, int months)
        {
            var interestRate = _bankInterestProvider.InterestRate;

            var administrationFee = CalculateAdministrationFee(principalAmount);
            
            var q = 1 + interestRate / (12 * 100);
            var qPowN = Math.Pow((double)q, months);

            var monthlyPayment = (decimal)((double)principalAmount * qPowN * (((double) q - 1) / (qPowN - 1)));
            
            return new Loan
            {
                OriginalPrincipal = principalAmount,
                Months = months,
                AdministrationFee = administrationFee,
                MonthlyPayment = monthlyPayment,
            };
        }

        private decimal CalculateAdministrationFee(decimal amount)
        {
            var percentageAmount = (amount / 100) * _bankInterestProvider.AdministrationFeePercentage;
            return percentageAmount < _bankInterestProvider.AdministrationFeeMaximumAmount
                ? percentageAmount 
                : _bankInterestProvider.AdministrationFeeMaximumAmount;
        }
    }



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