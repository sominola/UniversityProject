namespace UniversityProject.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
    ILessonRepository LessonRepository { get; }
    Task SaveAsync();
}