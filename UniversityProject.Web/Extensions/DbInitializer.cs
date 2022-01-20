using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityProject.Data.Constants;
using UniversityProject.Data.Context;
using UniversityProject.Data.Entities;

namespace UniversityProject.Web.Extensions;

public static class DbInitializer
{
    public static async Task InitializeDb(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();
        await Initialize(context);
    }

    private static async Task Initialize(AppDbContext context)
    {
        await context.Database.EnsureCreatedAsync();
        await InitUsers(context);
        await InitLessons(context);
        await context.SaveChangesAsync();
    }

    private static async Task InitLessons(AppDbContext context)
    {
        if (await context.Lessons.AnyAsync()) return;
        var lessonFaker = new Faker<Lesson>()
            .RuleFor(x => x.Name, y => y.Lorem.Word());

        var lessons = lessonFaker.Generate(20);
        
        await context.Lessons.AddRangeAsync(lessons);
    }

    private static async Task InitUsers(AppDbContext context)
    {
        if (await context.Users.AnyAsync()) return;
        var passwordHasher = new PasswordHasher<User>();
        var password = (User user) => passwordHasher.HashPassword(user, "password");
        var roles = await context.Roles.ToListAsync();
        var userFaker = new Faker<User>("ru")
            .RuleFor(x => x.Email, y => y.Internet.Email())
            .RuleFor(x => x.FirstName, y => y.Name.FirstName())
            .RuleFor(x => x.LastName, y => y.Name.LastName());


            var students = userFaker
            .RuleFor(x => x.Roles, new List<Role> {roles.FirstOrDefault(x => x.Name == UserRole.Student)})
            .Generate(25);
            students.ForEach(x=>x.HashedPassword=password(x));

            var teachers = userFaker
            .RuleFor(x => x.Roles, new List<Role> {roles.FirstOrDefault(x => x.Name == UserRole.Teacher)})
            .Generate(7);
            teachers.ForEach(x=>x.HashedPassword=password(x));

            var user = new User
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin",
                Roles = new List<Role> {roles.FirstOrDefault(x => x.Name == UserRole.Admin)},
            };
        user.HashedPassword =passwordHasher.HashPassword(user, "admin");
        await context.Users.AddAsync(user);
        await context.Users.AddRangeAsync(students);
        await context.Users.AddRangeAsync(teachers);
    }
}