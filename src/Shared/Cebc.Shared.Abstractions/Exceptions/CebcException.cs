using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cebc.Shared.Abstractions.Exceptions
{
    public abstract class CebcException : Exception
    {
        protected CebcException(string message) : base(message)
        {
        }
    }

    public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
}
