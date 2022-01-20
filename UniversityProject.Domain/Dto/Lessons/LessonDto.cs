
using UniversityProject.Domain.Dto.User;

namespace UniversityProject.Domain.Dto.Lessons;

public class LessonDto
{
  public long Id { get; set; }
  public string Name { get; set; }
  public List<UserDto> Students { get; set; }
  public List<UserDto> Teachers { get; set; }
}