using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Data.Constants;
using UniversityProject.Domain.Dto.Lessons;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Web.Pages.Lesson;

[Authorize(Roles = UserRole.Admin)]
public class EditModel : PageModel
{
    private readonly ILessonService _lessonService;
    public EditModel(ILessonService lessonService) => _lessonService = lessonService;

    [BindProperty] public CreateLessonDto LessonDto { get; set; }

    public async Task OnGetAsync(long lessonId)
    {
        var lesson = await _lessonService.GetLessonById(lessonId);
        LessonDto ??= new();
        LessonDto.Id = lesson.Id;
        LessonDto.Name = lesson.Name;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _lessonService.UpdateLessonAsync(LessonDto);
        return RedirectToPage("/Lessons/Index");
    }
}