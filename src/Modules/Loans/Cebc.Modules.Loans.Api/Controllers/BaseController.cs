using Cebc.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Cebc.Modules.Loans.Api.Controllers
{
    [ApiController]
    [ProducesDefaultContentType]
    public class BaseController : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is null)
            {
                return NotFound();
            }

            return Ok(model);
        }
    }
}