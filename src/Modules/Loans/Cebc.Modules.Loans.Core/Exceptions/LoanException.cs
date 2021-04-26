using Cebc.Shared.Abstractions.Exceptions;

namespace Cebc.Modules.Loans.Core.Exceptions
{
    public class LoanException : CebcException
    {
        public LoanException(string message) : base(message)
        {
        }
    }
}