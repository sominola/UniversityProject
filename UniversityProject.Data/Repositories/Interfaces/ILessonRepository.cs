using UniversityProject.Data.Entities;

namespace UniversityProject.Data.Repositories.Interfaces;

public interface ILessonRepository:IRepository<Lesson>
{
    Task<Lesson> GetLessonsByIdAsync(long id);
    Task<List<Lesson>> GetLessonsExcludeStudentAsync(long studentId);
    Task<List<Lesson>> GetLessonsByStudentAsync(long studentId);
    Task<List<Lesson>> GetLessonsExcludeTeacherAsync(long teacherId);
    Task<List<Lesson>> GetLessonsByTeacherAsync(long teacherId);
    Task AddStudentToLessonAsync(long lessonId, long studentId);
    Task RemoveStudentFromLessonAsync(long lessonId, long studentId);
    Task AddTeacherToLessonAsync(long lessonId, long teacherId);
    Task RemoveTeacherFromLessonAsync(long lessonId, long teacherId);
    Task<List<User>> GetStudentsExceptLessonAsync(long lessonId);
    Task<List<User>> GetTeachersExceptLessonAsync(long lessonId);
    Task UpdateLesson(long lessonId, string name);
}