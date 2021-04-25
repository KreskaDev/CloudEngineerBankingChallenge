using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cebc.Modules.Loans.Api.Controllers
{
    [Route(LoansModule.BasePath)]
    public class LoansController: BaseController
    {
        public async Task<ActionResult<LoanProposalDto>> ProposeLoan()
            => OkOrNotFound(new LoanProposalDto());
    }
}
