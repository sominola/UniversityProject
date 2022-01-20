using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Data.Exceptions;
using UniversityProject.Domain.Exceptions;

namespace UniversityProject.Web.Filters;

public class ExceptionHandlerFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;
        if (exception is DbNotFoundException)
        {
            context.ExceptionHandled = true;
            context.Result = new NotFoundResult();
        
            return Task.CompletedTask;
        }

        if (exception is ResultException)
        {
            context.ExceptionHandled = true;
            switch (exception)
            {
                case PageResultException pageException:
                    context.HttpContext.Response.StatusCode = (int) pageException.Code;
                    context.ModelState.AddModelError("", pageException.Message);
                    context.Result = new PageResult();
                    break;
                case CodePageResultException codePageException:
                    context.Result = new StatusCodeResult((int) codePageException.Code);
                    break;
                case ApiResultException apiException:
                    context.HttpContext.Response.WriteAsJsonAsync(apiException.Message);
                    break;
            }
        }
        


        return Task.CompletedTask;
    }
}