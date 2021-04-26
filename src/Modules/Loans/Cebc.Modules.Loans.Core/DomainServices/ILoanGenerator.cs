using Cebc.Modules.Loans.Core.Entities;

namespace Cebc.Modules.Loans.Core.DomainServices
{
    public interface ILoanGenerator
    {
        Loan GenerateLoan(decimal principalAmount, int months);
    }
}
