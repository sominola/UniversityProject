using Microsoft.EntityFrameworkCore;
using UniversityProject.Data.Context;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Repositories.Interfaces;

namespace UniversityProject.Data.Repositories;

public class UserRepository: Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext db) : base(db){}


    public async Task<User> GetCredentialUserAsync(string email)
    {
        return await Db.Users.Include(x=>x.Roles).FirstOrDefaultAsync(x => x.Email == email);
    }
    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await Db.Users.AnyAsync(account => account.Email == email);
    }
}