using System.Threading.Tasks;
using Cebc.Modules.Loans.Core.Dto;
using Cebc.Modules.Loans.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cebc.Modules.Loans.Api.Controllers
{
    [Route(LoansModule.BasePath)]
    public class LoansController: BaseController
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public async Task<ActionResult<LoanProposalDto>> ProposeLoan(decimal principal, int months)
            => OkOrNotFound(_loanService.ProposeLoan(principal, months));
    }
}
