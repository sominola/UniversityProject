using System.Net;

namespace UniversityProject.Domain.Exceptions;

public class PageResultException:ResultException
{
    public PageResultException(string message, HttpStatusCode code) : base(message, code)
    {
    }
}