using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Web.Pages.Account;

public class LogoutModel : PageModel
{
    private readonly IAuthService _authService;

    public LogoutModel(IAuthService authService)
    {
        _authService = authService;
    }
    
    public IActionResult OnGet() => NotFound();
    public async Task<IActionResult> OnPostAsync()
    {
        await _authService.Logout();
        return RedirectToPage("/Index");
    }
}