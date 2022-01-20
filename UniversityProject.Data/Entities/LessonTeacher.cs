namespace UniversityProject.Data.Entities;

public class LessonTeacher
{
    public long LessonId { get; set; }
    public Lesson Lesson { get; set; }    
    
    public long TeacherId { get; set; }
    public User Teacher { get; set; }
}