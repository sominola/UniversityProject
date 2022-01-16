using UniversityProject.Data.Entities;
using UniversityProject.Domain.Dto.Lessons;

namespace UniversityProject.Domain.Services.Interfaces;

public interface ILessonService
{
    public Task<LessonIndexDto> GetLessonExcludeCurrentUser();
}