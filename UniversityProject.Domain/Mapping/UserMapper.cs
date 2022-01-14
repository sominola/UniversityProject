using AutoMapper;
using UniversityProject.Data.Entities;
using UniversityProject.Domain.Dto.User;

namespace UniversityProject.Domain.Mapping;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<RegisterUserDto, User>().ReverseMap();
        CreateMap<RegisterUserDto, LoginDto>().ReverseMap();
        // .ForMember(dto => dto.Email, opt => opt.MapFrom(x => x.Email))
        // .ForMember(dto => dto.FirstName, opt => opt.MapFrom(x => x.FirstName))
        // .ForMember(dto => dto.LastName, opt => opt.MapFrom(x => x.LastName));
    }
}