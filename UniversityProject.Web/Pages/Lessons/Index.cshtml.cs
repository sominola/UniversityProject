using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Data.Constants;
using UniversityProject.Domain.Dto.Lessons;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Web.Pages.Lessons;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ILessonService _lessonService;
    [BindProperty] 
    public LessonsUserDto LessonsUserDto { get; set; }

    public IndexModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public async Task OnGetAsync()
    {
        LessonsUserDto = await _lessonService.GetLessons();
    }

    public async Task<IActionResult> OnPostJoinAsync(long lessonId)
    {
        if (User.IsInRole(UserRole.Student))
            await _lessonService.AddStudentToLessonAsync(lessonId);
        if (User.IsInRole(UserRole.Teacher))
            await _lessonService.AddTeacherToLessonAsync(lessonId);
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostLeaveAsync(long lessonId)
    {
        if (User.IsInRole(UserRole.Student))
            await _lessonService.RemoveStudentFromLessonAsync(lessonId);
        else if (User.IsInRole(UserRole.Teacher))
            await _lessonService.RemoveTeacherFromLessonAsync(lessonId);

        return RedirectToPage("Index");
    }
    
}