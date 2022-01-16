namespace UniversityProject.Data.Entities;

public class Lesson: BaseEntity
{
    public string Name { get; set; }
    public List<User> Users = new();
}