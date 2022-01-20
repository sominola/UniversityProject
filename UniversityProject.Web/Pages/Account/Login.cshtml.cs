using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Services.Interfaces;

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
            return RedirectToPage("/Index");
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
         await _authService.Login(LoginDto);
         return RedirectToPage("/Index");
    }
}