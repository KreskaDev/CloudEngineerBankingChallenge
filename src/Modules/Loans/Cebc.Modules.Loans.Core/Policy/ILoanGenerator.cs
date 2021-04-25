using Cebc.Modules.Loans.Core.Entities;

namespace Cebc.Modules.Loans.Core.Policy
{
    public interface ILoanGenerator
    {
        Loan GenerateLoan(decimal principalAmount, int months);
    }
}
