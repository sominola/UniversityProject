using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Models;

namespace UniversityProject.Domain.Services.Interfaces;

public interface IAuthService
{
    Task<IResult> Login(LoginDto model);
    Task<IResult> Login(string email, string password);

    Task<IResult> Logout();
}