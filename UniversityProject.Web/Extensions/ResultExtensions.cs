using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IResult = UniversityProject.Domain.Models.IResult;

namespace UniversityProject.Web.Extensions;

public static class ResultExtensions
{
    public static PageModel AddErrors(this PageModel pageModel, IResult result)
    {
        // pageResult.StatusCode((int)result.Error.Code);
        pageModel.Response.StatusCode = (int) result.Error.Code;
        pageModel.ModelState.AddModelError("", string.Join("\n", result.Error.Messages));
        return pageModel;
    }
}