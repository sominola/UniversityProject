using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Data.Constants;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Web.Pages.Lesson;

[Authorize(Roles = UserRole.Admin)]
public class DeleteModel : PageModel
{
    private readonly ILessonService _lessonService;

    public DeleteModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public async Task<IActionResult> OnPostAsync(long lessonId)
    {
        await _lessonService.RemoveLessonAsync(lessonId);
        return RedirectToPage("/Lessons/Index");
    }
}