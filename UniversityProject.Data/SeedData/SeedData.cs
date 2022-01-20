using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityProject.Data.Constants;
using UniversityProject.Data.Entities;

namespace UniversityProject.Data.SeedData;

public static class SeedData
{
    public static void Seed(this ModelBuilder builder)
    {
        SeedRoles(builder.Entity<Role>());
    }
    private static void SeedRoles(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role
            {
                Id = 1,
                Name = UserRole.Student
            },
            new Role
            {
                Id = 2,
                Name = UserRole.Teacher
            },
            new Role
            {
                Id = 3,
                Name = UserRole.Admin
            }
        );
    }
}