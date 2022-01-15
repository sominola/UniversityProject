using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Domain.Exceptions;

namespace UniversityProject.Web.Filters;

public class ExceptionHandlerFilter: IAsyncExceptionFilter
{

    public Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;
        context.ExceptionHandled = true;

        if (exception is ResultException resultException)
        {
            context.HttpContext.Response.StatusCode = (int)resultException.Code;
            if (resultException.IsApiException)
            {
                context.HttpContext.Response.WriteAsJsonAsync(exception.Message);
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)resultException.Code;
                context.ModelState.AddModelError("",resultException.Message);
                context.Result = new PageResult();
            }
         
        }
        else
        {
            context.HttpContext.Response.StatusCode = 500;
            context.HttpContext.Response.WriteAsJsonAsync(exception.Message);
        }

        return Task.CompletedTask;
    }
    
}