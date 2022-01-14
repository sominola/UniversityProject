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

    [BindProperty] 
    public RegisterUserDto RegisterDto { get; set; }

    public RegisterModel(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var resultCreateUser = await _userService.CreateUserAsync(RegisterDto);

        if (!resultCreateUser.IsSuccess)
        {
            this.AddErrors(resultCreateUser);
            return Page();
        }

        var resultLogin = await _authService.Login(RegisterDto.Email, RegisterDto.Password);
        if (resultLogin.IsSuccess)
        {
            return RedirectToPage("/test");
        }

        this.AddErrors(resultLogin);
        return Page();
    }
}