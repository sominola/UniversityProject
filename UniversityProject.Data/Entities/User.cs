namespace UniversityProject.Data.Entities;

public class User: BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    
    public DateTime RegisteredDate { get; set; }
    public List<Role> Roles { get; set; }
}