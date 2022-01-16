using UniversityProject.Domain.Dto.User;

namespace UniversityProject.Domain.Services.Interfaces;

public interface IUserService
{
    Task CreateUserAsync(RegisterUserDto model);
    Task<UpdateUserDto> GetCurrentUserAsync();
    Task UpdateUserAsync(UpdateUserDto model);
}