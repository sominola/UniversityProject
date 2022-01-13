using UniversityProject.Data.Models;
using UniversityProject.Domain.Dto.User;

namespace UniversityProject.Domain.Services.Interfaces;

public interface IUserService
{
    Task<Result<RegisterUserDto>> CreateUserAsync(RegisterUserDto model);
}