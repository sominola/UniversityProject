using UniversityProject.Data.Entities;

namespace UniversityProject.Data.Repositories.Interfaces;

public interface IUserRepository: IRepository<User>
{
    Task<bool> IsEmailTakenAsync(string email);
    Task<User> GetCredentialUserAsync(string email);
}