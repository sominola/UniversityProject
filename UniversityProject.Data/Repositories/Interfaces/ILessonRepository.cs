using UniversityProject.Data.Entities;

namespace UniversityProject.Data.Repositories.Interfaces;

public interface ILessonRepository:IRepository<Lesson>
{
    Task<List<Lesson>> GetLessonsExcludeUserAsync(long id);
    Task<List<Lesson>> GetLessonsByUserAsync(long id);
}