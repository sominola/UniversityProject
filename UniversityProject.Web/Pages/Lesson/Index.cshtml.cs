using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Data.Constants;
using UniversityProject.Domain.Dto.Lessons;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Web.Pages.Lesson;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ILessonService _lessonService;

    [BindProperty] public LessonDto LessonDto { get; set; }

    public IndexModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public async Task OnGetAsync(long lessonId)
    {
        LessonDto = await _lessonService.GetLessonById(lessonId);
    }

    public async Task<ActionResult> OnGetStudentsExcludeLessonAsync(long lessonId)
    {
        var users = await _lessonService.GetUsersExcludeLesson(lessonId);
        return new OkObjectResult(users);
    }

    public async Task<ActionResult> OnPostAddStudentLessonAsync(long lessonId, long studentId)
    {
        if (!User.IsInRole(UserRole.Admin)) return new ForbidResult();

        await _lessonService.AddStudentToLessonAsync(lessonId, studentId);
        return new OkResult();
    }

    public async Task<ActionResult> OnPostRemoveStudentLessonAsync(long lessonId, long studentId)
    {
        if (!User.IsInRole(UserRole.Admin)) return new ForbidResult();

        await _lessonService.RemoveStudentFromLessonAsync(lessonId, studentId);
        return new OkResult();
    }

    public async Task<ActionResult> OnPostAddTeacherLessonAsync(long lessonId, long teacherId)
    {
        if (!User.IsInRole(UserRole.Admin)) return new ForbidResult();

        await _lessonService.AddTeacherToLessonAsync(lessonId, teacherId);
        return new OkResult();
    }

    public async Task<ActionResult> OnPostRemoveTeacherLessonAsync(long lessonId, long teacherId)
    {
        if (!User.IsInRole(UserRole.Admin)) return new ForbidResult();

        await _lessonService.RemoveTeacherFromLessonAsync(lessonId, teacherId);
        return new OkResult();
    }
}