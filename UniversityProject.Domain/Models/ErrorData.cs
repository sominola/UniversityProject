namespace UniversityProject.Domain.Models;

public class ErrorData
{
    public ErrorCode Code { get; set; }
    public IEnumerable<string> Messages { get; set; }
}