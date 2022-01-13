using Microsoft.AspNetCore.Mvc;
using UniversityProject.Data.Models;

namespace UniversityProject.Web.Extensions;

public static class ResultExtensionsMethods
{
    public static ActionResult ToActionResult<T>(this Result<T> result)
    {
        if (Equals(result, null))
        {
            result = new Result<T>
            {
                Error = new ErrorData()
                {
                    Code = ErrorCode.InternalServerError,
                    Message = "No data received while processing data model"
                }
            };
            return new JsonResult(result.Error)
            {
                StatusCode = 500
            };
        }

        if (result.Error != null)
        {
            return result.Error.Code switch
            {
                ErrorCode.Unauthorized => new JsonResult(result.Error)
                {
                    StatusCode = 401
                },
                ErrorCode.ValidationError => new JsonResult(result.Error)
                {
                    StatusCode = 400
                },
                ErrorCode.InternalServerError => new JsonResult(result.Error)
                {
                    StatusCode = 500
                },
                ErrorCode.NotFound => new JsonResult(result.Error)
                {
                    StatusCode = 404
                },
                ErrorCode.UnprocessableEntity => new JsonResult(result.Error)
                {
                    StatusCode = 422
                },
                ErrorCode.Conflict => new JsonResult(result.Error)
                {
                    StatusCode = 409
                },
                _ => new JsonResult(result.Error)
                {
                    StatusCode = 500
                }
            };
        }

        return new OkResult();
    }
}