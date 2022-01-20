namespace UniversityProject.Domain.Dto.Lessons;

public class LessonCabinetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountStudents { get; set; }
    public bool IsUserLesson { get; set; }
}