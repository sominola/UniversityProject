using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Web.Pages.Account;
[Authorize]
public class AccountModel : PageModel
{
    private readonly IUserService _userService;

    [BindProperty]
    public UpdateUserDto UpdateUserDto { get; set; } 
    
    public AccountModel(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _userService.UpdateCredentialsAsync(UpdateUserDto);
        return RedirectToPage("/Account/Index");
    }
    
    public async Task OnGetAsync()
    {
       UpdateUserDto =  await _userService.GetCurrentUserAsync();
    }
}