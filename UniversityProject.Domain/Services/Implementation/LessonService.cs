using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniversityProject.Data.Repositories.Interfaces;
using UniversityProject.Domain.Dto.Lessons;
using UniversityProject.Domain.Extensions;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Domain.Services.Implementation;

public class LessonService:ILessonService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _context;
    public LessonService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor context)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _context = context;
    }
    
    public async Task<LessonIndexDto> GetLessonExcludeCurrentUser()
    {
        var id = _context.HttpContext.User.GetCurrentUserId();
        var userLessons = await _unitOfWork.LessonRepository.GetLessonsByUserAsync(id);
        var lessons = await _unitOfWork.LessonRepository.GetLessonsExcludeUserAsync(id);
        
        return new LessonIndexDto
        {
            UserLessons = userLessons,
            Lessons = lessons
        };
    }
}