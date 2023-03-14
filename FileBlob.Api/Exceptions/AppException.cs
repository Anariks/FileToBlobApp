using Models;

namespace Exceptions;

public class AppException : Exception
{
    public ErrorCode ErrorCode { get; }

    public AppException(ErrorCode errorCode)
        : this(errorCode, null) { }

    public AppException(ErrorCode errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }
}
