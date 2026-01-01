using System.Net;

namespace Postify.Shared.Kernel.Errors;

public class ServiceErrorException : Exception
{
    public Error Error { get; init; }
    public HttpStatusCode HttpStatusCode { get; }

    public ServiceErrorException(Error error) : base(error.ToString())
    {
        Error = error;
        HttpStatusCode = Error.Type switch
        {
            ErrorType.Failure => HttpStatusCode.UnprocessableEntity,
            ErrorType.Unexpected => HttpStatusCode.UnprocessableEntity,
            ErrorType.NotFound => HttpStatusCode.NotFound,
            ErrorType.Validation => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };
    }
}
