using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Web.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly IUserService _userService;

    [BindProperty] public RegisterUserDto RegisterDto { get; set; }

    public RegisterModel(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        
        var result = await _userService.CreateUserAsync(RegisterDto);

        if (result.Error == null)
        {
            return RedirectToPage("/test");
        }

        ModelState.AddModelError("", result.Error.Message);

        return Page();
    }
}