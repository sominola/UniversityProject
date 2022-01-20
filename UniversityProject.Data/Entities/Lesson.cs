namespace UniversityProject.Data.Entities;

public class Lesson: BaseEntity
{
    public string Name { get; set; }
    public List<User> Students { get; set; } = new();
    public List<User> Teachers { get; set; } = new();
}