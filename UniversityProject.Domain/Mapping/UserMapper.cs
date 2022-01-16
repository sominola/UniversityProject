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
        CreateMap<UpdateUserDto, User>().ReverseMap();
    }
}