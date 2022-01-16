using UniversityProject.Data.Entities;

namespace UniversityProject.Domain.Dto.Lessons;

public class LessonIndexDto
{
    public List<Lesson> UserLessons { get; set; } = new();
    public List<Lesson> Lessons { get; set; } = new();
}