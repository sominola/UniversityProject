using System.Net;

namespace UniversityProject.Domain.Exceptions;

public class ApiResultException: ResultException
{
    public ApiResultException(string message, HttpStatusCode code) : base(message, code)
    {
    }
}