using AutoMapper;
using UniversityProject.Data.Entities;
using UniversityProject.Domain.Dto.Lessons;

namespace UniversityProject.Domain.Mapping;

public class LessonMapper : Profile
{
    public LessonMapper()
    {
        CreateMap<List<Lesson>, LessonIndexDto>().ForMember(x => x.UserLessons, y => y.MapFrom(z => z)).ReverseMap();
    }
}