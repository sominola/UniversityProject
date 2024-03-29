﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UniversityProject.Web.Filters;

public class ModelValidationFilter: ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        var modelState = actionContext.ModelState;
        if (!modelState.IsValid)
            actionContext.Result = new BadRequestObjectResult(modelState);
    }
}