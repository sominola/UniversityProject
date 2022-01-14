using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Services.Interfaces;
using UniversityProject.Web.Extensions;

namespace UniversityProject.Web.Pages.Account;

public class LoginModel : PageModel
{
    private readonly IAuthService _authService;
    [BindProperty]
    public LoginDto LoginDto { get; set; }

    public LoginModel(IAuthService authService)
    {
        _authService = authService;
    }
    public IActionResult OnGet()
    {
        if (User.Identity!.IsAuthenticated)
            return RedirectToRoute("/");
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        
        var result = await _authService.Login(LoginDto);
        
        if (result.IsSuccess)
        {
            return RedirectToPage("Test");
        }
        this.AddErrors(result);
        return Page();
    }
}