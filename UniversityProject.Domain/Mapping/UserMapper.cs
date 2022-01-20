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
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<User, UpdateUserDto>()
            .ForMember(x => x.Roles, y => y.MapFrom(x => x.Roles));
        CreateMap<UpdateUserDto, User>()
            .ForMember(x => x.Roles, y => y.Ignore());
    }
}