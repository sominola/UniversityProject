using Microsoft.EntityFrameworkCore;
using UniversityProject.Data.Constants;
using UniversityProject.Data.Context;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Exceptions;
using UniversityProject.Data.Repositories.Interfaces;

namespace UniversityProject.Data.Repositories;

public class LessonRepository : Repository<Lesson>, ILessonRepository
{
    public LessonRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<Lesson> GetLessonsByIdAsync(long lessonId)
    {
        var lesson = await GetQueryableNoTracking().Include(x => x.Students).Include(x => x.Teachers)
            .FirstOrDefaultAsync(x => x.Id == lessonId);
        if (lesson == null)
            throw new DbNotFoundException("Lesson not found");
        return lesson;
    }

    public async Task<List<Lesson>> GetLessonsExcludeStudentAsync(long studentId)
    {
        return await GetQueryableNoTracking().Include(x => x.Students)
            .Where(x => x.Students.All(y => y.Id != studentId)).ToListAsync();
    }

    public async Task<List<Lesson>> GetLessonsByStudentAsync(long studentId)
    {
        return await GetQueryableNoTracking().Include(x => x.Students)
            .Where(lesson => lesson.Students.Any(user => user.Id == studentId)).ToListAsync();
    }


    public async Task<List<Lesson>> GetLessonsExcludeTeacherAsync(long teacherId)
    {
        return await GetQueryableNoTracking().Include(x => x.Students).Include(x => x.Teachers)
            .Where(x => x.Teachers.All(y => y.Id != teacherId) && x.Teachers.Count < 2).ToListAsync();
    }

    public async Task<List<Lesson>> GetLessonsByTeacherAsync(long teacherId)
    {
        return await GetQueryableNoTracking().Include(x => x.Teachers).Include(x => x.Students)
            .Where(lesson => lesson.Teachers.Any(user => user.Id == teacherId)).ToListAsync();
    }

    public async Task AddStudentToLessonAsync(long lessonId, long studentId)
    {
        var lesson = await GetByIdAsync(lessonId);
        if (lesson == null)
            throw new DbNotFoundException("Lesson not found");

        var user = await Db.Users.Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == studentId && x.Roles.Any(y => y.Name == UserRole.Student));
        if (user == null)
            throw new DbNotFoundException("User not found");

        lesson.Students = new List<User>
        {
            user
        };
    }

    public async Task RemoveStudentFromLessonAsync(long lessonId, long studentId)
    {
        var lesson = await Db.Lessons.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == lessonId);
        if (lesson == null)
            throw new DbNotFoundException("Lesson not found");

        var user = await Db.Users.Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == studentId && x.Roles.Any(y => y.Name == UserRole.Student));
        if (user == null)
            throw new DbNotFoundException("User not found");

        lesson.Students.Remove(user);
    }

    public async Task AddTeacherToLessonAsync(long lessonId, long teacherId)
    {
        var lesson = await Db.Lessons.Include(x => x.Teachers).FirstOrDefaultAsync(x => x.Id == lessonId);
        if (lesson == null)
            throw new DbNotFoundException("Lesson not found");
        if (lesson.Teachers.Count == 2)
            throw new DbNotFoundException("Max two teachers");
        var user = await Db.Users.Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == teacherId && x.Roles.Any(y => y.Name == UserRole.Teacher));
        if (user == null)
            throw new DbNotFoundException("User not found");

        lesson.Teachers.Add(user);
    }

    public async Task RemoveTeacherFromLessonAsync(long lessonId, long teacherId)
    {
        var lesson = await Db.Lessons.Include(x => x.Teachers).FirstOrDefaultAsync(x => x.Id == lessonId);
        if (lesson == null)
            throw new DbNotFoundException("Lesson not found");

        var user = await Db.Users.Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == teacherId && x.Roles.Any(y => y.Name == UserRole.Teacher));
        if (user == null)
            throw new DbNotFoundException("User not found");

        lesson.Teachers.Remove(user);
    }

    public async Task<List<User>> GetStudentsExceptLessonAsync(long lessonId)
    {
        return await Db.Users.AsNoTracking().Include(x=>x.Roles).Include(x => x.Lessons)
            .Where(x=>x.Roles.Any(y=>y.Name == UserRole.Student))
            .Where(x => x.Lessons.Any(y => y.Id != lessonId) || !x.Lessons.Any()).ToListAsync();
    }

    public async Task<List<User>> GetTeachersExceptLessonAsync(long lessonId)
    {
        var allTeacher =  Db.Users.AsNoTracking().Include(x => x.Roles).Where(x => x.Roles.Any(y => y.Name == UserRole.Teacher));
        var teachersLesson =  GetQueryableNoTracking().Include(x => x.Teachers).Where(x => x.Id == lessonId).SelectMany(x=>x.Teachers);
        return await allTeacher.Where(x => teachersLesson.All(y => y.Id != x.Id)).ToListAsync();
    }

    public async Task UpdateLesson(long lessonId, string name)
    {
        var lesson = await GetByIdAsync(lessonId);
        if (lesson == null)
            throw new DbNotFoundException("Lesson not found");
        if (!string.IsNullOrEmpty(name))
            lesson.Name = name;
    }
}