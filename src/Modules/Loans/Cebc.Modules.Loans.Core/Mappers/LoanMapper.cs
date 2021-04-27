using System;
using Cebc.Modules.Loans.Core.Dto;
using Cebc.Modules.Loans.Core.Entities;

namespace Cebc.Modules.Loans.Core.Mappers
{
    public interface ILoanMapper
    {
        LoanProposalDto Map(Loan loan);
    }

    public class LoanMapper : ILoanMapper
    {
        public LoanProposalDto Map(Loan loan) 
            => new()
            {
                LoanSpecification = new LoanSpecificationDto
                {
                    OriginalPrincipal = CurrencyFormatter(loan.Specification.OriginalPrincipal),
                    DurationInMonths = loan.Specification.DurationInMonths,
                    AnnualInterestRate = IndicatorsFormatter(loan.Specification.AnnualInterestRate),
                    CompoundFrequency = loan.Specification.CompoundFrequency,
                },
                LoanIndicators = new LoanIndicatorsDto
                {
                    AnnualPercentageRate = IndicatorsFormatter(loan.Indicators.AnnualPercentageRate),
                    EffectiveAnnualPercentageRate = IndicatorsFormatter(loan.Indicators.EffectiveAnnualPercentageRate),
                    EffectiveAnnualRate = IndicatorsFormatter(loan.Indicators.EffectiveAnnualRate),
                },
                LoanSummary = new LoanSummaryDto
                {
                    Installment = CurrencyFormatter(loan.Installment),
                    AdministrationFee = CurrencyFormatter(loan.AdministrationFee),
                    TotalInterest = CurrencyFormatter(loan.TotalInterest),
                    FinanceCharge = CurrencyFormatter(loan.FinanceCharge),
                    TotalAmountPaid = CurrencyFormatter(loan.TotalAmountPaid)
                }
            };

        //Move to CebcFormatter class
        decimal CurrencyFormatter(decimal value) => Math.Round(value, 2);
        decimal IndicatorsFormatter(decimal value) => Math.Round(value, 3);
    }
}