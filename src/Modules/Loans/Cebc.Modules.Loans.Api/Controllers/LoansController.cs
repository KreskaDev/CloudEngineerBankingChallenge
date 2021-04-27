using System.Net.Mime;
using Cebc.Modules.Loans.Core.ApplicationServices;
using Cebc.Modules.Loans.Core.Dto;
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

        [HttpGet("ProposeLoan")]
        [ProducesResponseType(200)]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<LoanProposalDto> ProposeLoan([FromQuery] ProposeLoanDto proposeLoanDto)
            => OkOrNotFound(_loanService.ProposeLoan(proposeLoanDto));
    }
}
