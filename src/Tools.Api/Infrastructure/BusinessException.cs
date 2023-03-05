using Microsoft.AspNetCore.Http;

namespace IGeekFan.FreeKit.Infrastructure.Exceptions;

/// <summary>
/// Exception type for app exceptions
/// </summary>
[Serializable]
public class BusinessException : Exception
{
    public int StatusCode { get; set; }
    public BusinessException()
    { }

    public BusinessException(string message, int statusCode = StatusCodes.Status400BadRequest, Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}
