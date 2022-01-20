using Microsoft.EntityFrameworkCore;
using UniversityProject.Data.Context;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Repositories.Interfaces;

namespace UniversityProject.Data.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<List<Role>> GetRolesByUserAsync(long userId)
    {
        return await GetQueryableNoTracking().Include(x => x.Users).Where(x => x.Users.Any(user => user.Id == userId))
            .ToListAsync();
    }
}