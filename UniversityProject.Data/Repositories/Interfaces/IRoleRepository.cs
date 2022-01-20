using UniversityProject.Data.Entities;

namespace UniversityProject.Data.Repositories.Interfaces;

public interface IRoleRepository:IRepository<Role>
{
    Task<List<Role>> GetRolesByUserAsync(long userId);
}