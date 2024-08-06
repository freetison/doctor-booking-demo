using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Api.Common.Exceptions;

[ExcludeFromCodeCoverage]
public abstract class CustomException : Exception
{
    protected CustomException()
    {
    }

    protected CustomException(int code, string message) : base(message)
    {
        HResult = code;
    }

    protected CustomException(int code, string message, Exception inner) : base(message, inner)
    {
        HResult = code;
    }

    public abstract HttpStatusCode HttpStatusCode { get; set; }
    public abstract int ErrorCode { get; set; }

}