using System.Net;

namespace UniversityProject.Domain.Exceptions;

public class CodePageResultException:ResultException
{
    public CodePageResultException(string message, HttpStatusCode code) : base(message, code)
    {
    }
}