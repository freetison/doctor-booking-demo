using System.Net;

namespace Api.Common.Exceptions;

public class NotFoundException : CustomException
{
    private const int ERROR_CODE = (int)HttpStatusCode.NotFound;

    public override HttpStatusCode HttpStatusCode { get; set; }
    public override int ErrorCode { get; set; } = ERROR_CODE;

    public NotFoundException() : base(ERROR_CODE, "Resource not found") => HResult = ERROR_CODE;

    public NotFoundException(string message) : base(ERROR_CODE, message) => HResult = ERROR_CODE;

}