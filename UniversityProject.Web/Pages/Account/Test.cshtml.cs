using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UniversityProject.Web.Pages.Account;

[Authorize(Roles = "Admin")]
public class TestModel : PageModel
{
    public void OnGet()
    {
    }
}