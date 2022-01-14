namespace UniversityProject.Domain.Models;

public interface IResult
{
    ErrorData Error { get; }
    bool IsSuccess { get; }
    IResult Success();
    IResult SetError(ErrorCode code, string message);
    IResult SetError(ErrorCode code, IEnumerable<string> messages);
}

public interface IResult<T> : IResult
{
    T Data { get; }
    IResult<T> Success(T data);
    new IResult<T> SetError(ErrorCode code, string message);
    new IResult<T> SetError(ErrorCode code, IEnumerable<string> messages);
}