using AutoMapper;
using UniversityProject.Data.Entities;
using UniversityProject.Domain.Dto.Lessons;

namespace UniversityProject.Domain.Mapping;

public class LessonMapper : Profile
{
    public LessonMapper()
    {
        CreateMap<Lesson, LessonCabinetDto>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
            .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
            .ForMember(x => x.CountStudents, y => y.MapFrom(z => z.Students.Count));
        CreateMap<Lesson, LessonDto>()
            .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
            .ForMember(x => x.Students, y => y.MapFrom(z => z.Students))
            .ForMember(x => x.Teachers, y => y.MapFrom(z => z.Teachers));
    }
}