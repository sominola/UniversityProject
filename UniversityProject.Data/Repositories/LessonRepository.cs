using Microsoft.EntityFrameworkCore;
using UniversityProject.Data.Context;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Repositories.Interfaces;

namespace UniversityProject.Data.Repositories;

public class LessonRepository:Repository<Lesson>,ILessonRepository
{
    public LessonRepository(AppDbContext db) : base(db) { }

    public async Task<List<Lesson>> GetLessonsExcludeUserAsync(long id)
    {
        return await GetQueryableNoTracking().Include(x => x.Users).Where(x => x.Users.All(y=>y.Id != id)).ToListAsync();
    }

    public async Task<List<Lesson>> GetLessonsByUserAsync(long id)
    {
        return await GetQueryableNoTracking().Include(x => x.Users)
            .Where(lesson => lesson.Users.Any(user => user.Id == id)).ToListAsync();
    }
}