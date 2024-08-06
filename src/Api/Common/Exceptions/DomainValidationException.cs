using System.Net;

namespace Api.Common.Exceptions;

public class DomainValidationException : CustomException
{
    private const int ERROR_CODE = (int)HttpStatusCode.BadRequest;

    public override HttpStatusCode HttpStatusCode { get; set; }
    public override int ErrorCode { get; set; } = ERROR_CODE;

    public DomainValidationException() : base(ERROR_CODE, "Bad request on validation") => HResult = ERROR_CODE;

    public DomainValidationException(string message) : base(ERROR_CODE, message) => HResult = ERROR_CODE;

}