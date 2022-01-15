using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Services.Interfaces;
using UniversityProject.Web.Extensions;

namespace UniversityProject.Web.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    [BindProperty] public RegisterUserDto RegisterDto { get; set; }

    public RegisterModel(IUserService userService, IAuthService authService)
    {
        _userService = userService;
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
        await _userService.CreateUserAsync(RegisterDto);
        await _authService.Login(RegisterDto.Email, RegisterDto.Password);
        return RedirectToPage("/index");
    }
}