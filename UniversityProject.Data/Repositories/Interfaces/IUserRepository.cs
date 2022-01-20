using UniversityProject.Data.Entities;

namespace UniversityProject.Data.Repositories.Interfaces;

public interface IUserRepository: IRepository<User>
{
    Task<bool> IsEmailTakenAsync(string email);
    Task<string> GetEmailByAsync(long userId);
    Task<User> GetCredentialUserAsync(string email);
    Task UpdateCredentialsAsync(User user);

}