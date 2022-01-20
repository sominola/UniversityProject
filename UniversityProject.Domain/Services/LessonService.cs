using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniversityProject.Data.Constants;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Repositories.Interfaces;
using UniversityProject.Domain.Dto.Lessons;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Extensions;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Domain.Services;

public class LessonService : ILessonService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _context;

    public LessonService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor context, IUserService userService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<LessonDto> GetLessonById(long lessonId)
    {
        var lesson = await _unitOfWork.LessonRepository.GetLessonsByIdAsync(lessonId);
        return _mapper.Map<LessonDto>(lesson);
    }

    public async Task<LessonDto> GetUsersExcludeLesson(long lessonId)
    {
        var students =
            _mapper.Map<List<UserDto>>(await _unitOfWork.LessonRepository.GetStudentsExceptLessonAsync(lessonId));
        var teachers =
            _mapper.Map<List<UserDto>>(await _unitOfWork.LessonRepository.GetTeachersExceptLessonAsync(lessonId));

        return new LessonDto
        {
            Id = lessonId,
            Students = students,
            Teachers = teachers,
        };
    }

    public async Task<LessonsUserDto> GetLessons()
    {
        var (userLessons, otherLessons) = await GetUserLessons();
        var otherLessonsMap = _mapper.Map<List<Lesson>, List<LessonCabinetDto>>(otherLessons);
        var userLessonsMap = _mapper.Map<List<Lesson>, List<LessonCabinetDto>>(userLessons);
        userLessonsMap.ForEach(x => x.IsUserLesson = true);

        var list = userLessonsMap.Concat(otherLessonsMap).OrderByDescending(x=>x.CountStudents).ThenBy(x=>x.Id).ToList();
        return new LessonsUserDto
        {
            Lessons = list
        };
    }

    private async Task<(List<Lesson> userLessons, List<Lesson> otherLessons)> GetUserLessons()
    {
        var id = _context.HttpContext.User.GetCurrentUserId();
        List<Lesson> userLessons = null;
        List<Lesson> otherLessons = null;
        if (_context.HttpContext.User.IsInRole(UserRole.Admin))
        {
            userLessons = await _unitOfWork.LessonRepository.GetQueryableNoTracking().Include(x => x.Students)
                .ToListAsync();
            otherLessons = new();
        }
        else if (_context.HttpContext.User.IsInRole(UserRole.Student))
        {
            userLessons = await _unitOfWork.LessonRepository.GetLessonsByStudentAsync(id);
            otherLessons = await _unitOfWork.LessonRepository.GetLessonsExcludeStudentAsync(id);
        }
        else if (_context.HttpContext.User.IsInRole(UserRole.Teacher))
        {
            userLessons = await _unitOfWork.LessonRepository.GetLessonsByTeacherAsync(id);
            otherLessons = await _unitOfWork.LessonRepository.GetLessonsExcludeTeacherAsync(id);
        }

        return (userLessons, otherLessons);
    }

    public async Task AddTeacherToLessonAsync(long lessonId, long? teacherId)
    {
        teacherId ??= _context.HttpContext.User.GetCurrentUserId();
        await _unitOfWork.LessonRepository.AddTeacherToLessonAsync(lessonId, teacherId.Value);
        await _unitOfWork.SaveAsync();
    }

    public async Task RemoveTeacherFromLessonAsync(long lessonId, long? teacherId)
    {
        teacherId ??= _context.HttpContext.User.GetCurrentUserId();
        await _unitOfWork.LessonRepository.RemoveTeacherFromLessonAsync(lessonId, teacherId.Value);
        await _unitOfWork.SaveAsync();
    }

    public async Task AddStudentToLessonAsync(long lessonId, long? studentId)
    {
        studentId ??= _context.HttpContext.User.GetCurrentUserId();
        await _unitOfWork.LessonRepository.AddStudentToLessonAsync(lessonId, studentId.Value);
        await _unitOfWork.SaveAsync();
    }

    public async Task RemoveStudentFromLessonAsync(long lessonId, long? studentId)
    {
        studentId ??= _context.HttpContext.User.GetCurrentUserId();
        await _unitOfWork.LessonRepository.RemoveStudentFromLessonAsync(lessonId, studentId.Value);
        await _unitOfWork.SaveAsync();
    }

    public async Task CreateLessonAsync(string name)
    {
        await _unitOfWork.LessonRepository.AddAsync(new Lesson {Name = name});
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateLessonAsync(CreateLessonDto model)
    {
        await _unitOfWork.LessonRepository.UpdateLesson(model.Id, model.Name);
        await _unitOfWork.SaveAsync();
    }

    public async Task RemoveLessonAsync(long lessonId)
    {
        await _unitOfWork.LessonRepository.RemoveAsync(lessonId);
        await _unitOfWork.SaveAsync();
    }
}