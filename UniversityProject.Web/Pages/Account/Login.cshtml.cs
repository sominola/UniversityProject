using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Services.Implementation;

namespace UniversityProject.Web.Pages.Account;

public class LoginModel : PageModel
{
    private readonly AuthService _authService;
    [BindProperty]
    public LoginDto LoginDto { get; set; }
    public LoginModel(AuthService authService)
    {
        _authService = authService;
    }
    public void OnGet()
    {
        
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var result = await _authService.Login(LoginDto);
        if (result.Error == null)
        {
            HttpContext.Response.Cookies.Append("Token",result.Data);
            return RedirectToPage("Test");
        }
        else
        {
            ModelState.AddModelError("",result.Error.Message);
        }

        return Page();
    }
}