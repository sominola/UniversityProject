using Microsoft.EntityFrameworkCore;
using UniversityProject.Data.Context;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Repositories.Interfaces;

namespace UniversityProject.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext db) : base(db)
    {
    }

    public async Task UpdateCredentialsAsync(User user)
    {
        var userDb = await GetByIdAsync(user.Id);
        userDb.Email = user.Email;
        userDb.FirstName = user.FirstName;
        userDb.LastName = user.LastName;
    }

    public async Task<User> GetCredentialUserAsync(string email)
    {
        return await GetQueryableNoTracking().Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await Db.Users.AnyAsync(account => account.Email == email);
    }

    public async Task<string> GetEmailByAsync(long userId)
    {
        return (await GetQueryableNoTracking().FirstOrDefaultAsync(x => x.Id == userId))?.Email;
    }


}