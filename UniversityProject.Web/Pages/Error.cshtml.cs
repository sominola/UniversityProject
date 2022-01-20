using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UniversityProject.Web.Pages;
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class ErrorModel : PageModel
{
    public string ErrorStatusCode { get; set; }
    public string Message { get; set; }

    public void OnGet(string code)
    {
        ErrorStatusCode = code;
        Message = code switch
        {
            "401" => "Please authorize",
            "403" => "Access denied",
            "404" => "Not found!",
            _ => Message
        };
    }
}