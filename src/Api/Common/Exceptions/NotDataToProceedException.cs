using System.Net;

namespace Api.Common.Exceptions;

public class NotDataToProceedException : CustomException
{
    private const int ERROR_CODE = (int)HttpStatusCode.NoContent;

    public override HttpStatusCode HttpStatusCode { get; set; }
    public override int ErrorCode { get; set; } = ERROR_CODE;

    public NotDataToProceedException() : base(ERROR_CODE, "Not found data to proceed") => HResult = ERROR_CODE;

    public NotDataToProceedException(string message) : base(ERROR_CODE, message) => HResult = ERROR_CODE;

}