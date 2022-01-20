namespace UniversityProject.Data.Entities;

public class LessonUser
{
    public long LessonId { get; set; }
    public Lesson Lesson { get; set; }    
    
    public long StudentId { get; set; }
    public User Student { get; set; }
}