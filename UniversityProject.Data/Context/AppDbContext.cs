using Microsoft.EntityFrameworkCore;
using UniversityProject.Data.Configurations;
using UniversityProject.Data.Entities;

namespace UniversityProject.Data.Context;

public sealed class AppDbContext: DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Lesson> Lessons { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        builder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                Name = "User"
            },
            new Role
            {
                Id = 2,
                Name = "Admin"
            }
        );
    }
}