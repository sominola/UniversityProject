using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Data.Constants;

namespace UniversityProject.Web.Pages.Account;

[Authorize(Roles = UserRole.Student)]
public class TestModel : PageModel
{
    public void OnGet()
    {
    }
}