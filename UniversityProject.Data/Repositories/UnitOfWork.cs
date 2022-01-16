using UniversityProject.Data.Context;
using UniversityProject.Data.Repositories.Interfaces;

namespace UniversityProject.Data.Repositories;

public class UnitOfWork:IUnitOfWork
{
    private readonly AppDbContext _db;
    private IUserRepository _userRepository;
    private IRoleRepository _roleRepository;
    private ILessonRepository _lessonRepository;
    public UnitOfWork(AppDbContext applicationContext)
    {
        _db = applicationContext;
    }

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_db);
    public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_db);
    public ILessonRepository LessonRepository => _lessonRepository ??= new LessonRepository(_db);

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

}