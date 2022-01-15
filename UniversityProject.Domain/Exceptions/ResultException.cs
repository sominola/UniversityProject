using System.Net;

namespace UniversityProject.Domain.Exceptions;

public class ResultException: Exception
{
    public readonly HttpStatusCode Code;
    public readonly bool IsApiException;
    public ResultException(string message, HttpStatusCode code):base(message)
    {
        Code = code;
    }
    public ResultException(string message, HttpStatusCode code, bool isApiException):this(message,code)
    {
        IsApiException = isApiException;
    }
}