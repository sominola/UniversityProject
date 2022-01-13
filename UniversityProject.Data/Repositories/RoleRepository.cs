using UniversityProject.Data.Context;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Repositories.Interfaces;

namespace UniversityProject.Data.Repositories;

public class RoleRepository: Repository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext db) : base(db){}
    
    

}