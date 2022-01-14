namespace UniversityProject.Domain.Models;

public class Result<T> : Result, IResult<T>
{
    public T Data { get; private init; }
    public IResult<T> Success(T data)
    {
        return new Result<T>
        {
            Data = data,
            Error = new ErrorData
            {
                Code = ErrorCode.Ok
            }
        };
    }
    
    public override IResult<T> SetError(ErrorCode code, string message)
    {
        return new Result<T>
        {
            Error = new ErrorData
            {
                Code = code,
                Messages = new List<string>
                {
                    message
                }
            }
        };
    }
    
    public override IResult<T> SetError (ErrorCode code, IEnumerable<string> messages)
    {
        return new Result<T>
        {
            Error = new ErrorData
            {
                Code = code,
                Messages = messages
            }
        };
    }
}

public class Result: IResult
{
    public ErrorData Error { get; protected init; }
    public bool IsSuccess => Error?.Code == ErrorCode.Ok;
    public IResult Success()
    {
        return new Result
        {
            Error = new ErrorData
            {
                Code = ErrorCode.Ok
            }
        };
    }
    public virtual IResult SetError(ErrorCode code, string message)
    {
        return new Result
        {
            Error = new ErrorData
            {
                Code = code,
                Messages = new List<string>
                {
                    message
                }
            }
        };
    }
    public virtual IResult SetError(ErrorCode code, IEnumerable<string> messages)
    {
        return new Result
        {
            Error = new ErrorData
            {
                Code = code,
                Messages = messages
            }
        };
    }
}