using UniversityProject.Domain.Dto.Lessons;

namespace UniversityProject.Domain.Services.Interfaces;

public interface ILessonService
{
    Task<LessonDto> GetLessonById(long lessonId);
    Task<LessonDto> GetUsersExcludeLesson(long lessonId);
    Task<LessonsUserDto> GetLessons();
    Task AddTeacherToLessonAsync(long lessonId, long? teacherId = null);
    Task RemoveTeacherFromLessonAsync(long lessonId, long? teacherId = null);
    Task AddStudentToLessonAsync(long lessonId, long? studentId = null);
    Task RemoveStudentFromLessonAsync(long lessonId, long? studentId = null);
    Task CreateLessonAsync(string name);
    Task UpdateLessonAsync(CreateLessonDto lesson);
    Task RemoveLessonAsync(long lessonId);
}