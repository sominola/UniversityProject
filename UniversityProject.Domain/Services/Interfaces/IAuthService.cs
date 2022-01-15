using UniversityProject.Domain.Dto.User;

namespace UniversityProject.Domain.Services.Interfaces;

public interface IAuthService
{
    Task Login(LoginDto model);
    Task Login(string email, string password);

    Task Logout();
}