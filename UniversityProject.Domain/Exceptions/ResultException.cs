using System.Net;

namespace UniversityProject.Domain.Exceptions;

public class ResultException: Exception
{
    public readonly HttpStatusCode Code;

    protected ResultException(string message, HttpStatusCode code):base(message)
    {
        Code = code;
    }
}