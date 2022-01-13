namespace UniversityProject.Data.Models;

public enum ErrorCode
{
    ValidationError,
    Unauthorized,
    InternalServerError,
    NotFound,
    UnprocessableEntity,
    Conflict,
    ForgotPasswordExpired,
    Forbidden,
}