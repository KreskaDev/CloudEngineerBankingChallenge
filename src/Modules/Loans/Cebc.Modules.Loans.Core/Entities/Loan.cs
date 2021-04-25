using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cebc.Modules.Loans.Core.Entities
{
    public class Loan
    {
        public int Months { get; set; }
        public decimal OriginalPrincipal { get; set; }

        public decimal MonthlyPayment { get; set; }
        public decimal AdministrationFee { get; set; }

        public decimal MonthlyPaymentRounded => Math.Round(MonthlyPayment, 2);
        public decimal AdministrationFeeRounded => Math.Round(AdministrationFee, 2);
        public decimal TotalInterestRateRounded => Math.Round(TotalInterestRate, 2);

        public decimal TotalInterestRate 
            => MonthlyPayment * Months - OriginalPrincipal;


        public decimal TotalAmountPaidRounded
            => OriginalPrincipal + TotalInterestRateRounded + AdministrationFeeRounded;



    }
}
