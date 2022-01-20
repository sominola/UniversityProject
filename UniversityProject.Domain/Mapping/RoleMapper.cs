using AutoMapper;
using UniversityProject.Data.Entities;

namespace UniversityProject.Domain.Mapping;

public class RoleMapper:Profile
{
    public RoleMapper()
    {
       CreateMap<Role, string>().ConvertUsing(x=>x.Name);
    }
}