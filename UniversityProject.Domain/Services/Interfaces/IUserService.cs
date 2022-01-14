using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Models;

namespace UniversityProject.Domain.Services.Interfaces;

public interface IUserService
{
    Task<IResult> CreateUserAsync(RegisterUserDto model);
}