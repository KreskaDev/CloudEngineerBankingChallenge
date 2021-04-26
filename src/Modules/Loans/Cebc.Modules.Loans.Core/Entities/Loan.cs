using System;

namespace Cebc.Modules.Loans.Core.Entities
{
    public class Loan
    {
        public int Months { get; set; }
        public decimal OriginalPrincipal { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal AdministrationFee { get; set; }

        public decimal TotalInterestRate => MonthlyPayment * Months - OriginalPrincipal;
        public decimal TotalAmountPaid => OriginalPrincipal + TotalInterestRate + AdministrationFee;


        //------------
        decimal CurrencyFormatter(decimal amount) => Math.Round(amount, 2);

        public decimal MonthlyPaymentRounded => CurrencyFormatter(MonthlyPayment);
        public decimal AdministrationFeeRounded => CurrencyFormatter(AdministrationFee);
        public decimal TotalInterestRateRounded => CurrencyFormatter(TotalInterestRate);
        public decimal TotalAmountPaidRounded => CurrencyFormatter(TotalAmountPaid);
    }
}
