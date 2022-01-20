using Microsoft.EntityFrameworkCore;
using UniversityProject.Data.Configurations;
using UniversityProject.Data.Entities;
using UniversityProject.Data.SeedData;

namespace UniversityProject.Data.Context;

public sealed class AppDbContext: DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        builder.Seed();
    }
}