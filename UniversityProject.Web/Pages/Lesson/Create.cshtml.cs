using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Data.Constants;
using UniversityProject.Domain.Dto.Lessons;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Web.Pages.Lesson;
[Authorize(Roles = UserRole.Admin)]
public class CreateModel : PageModel
{
    private readonly ILessonService _lessonService;
    public CreateModel(ILessonService lessonService) => _lessonService = lessonService;

    [BindProperty] 
    public CreateLessonDto LessonDto { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        await _lessonService.CreateLessonAsync(LessonDto.Name);
        return RedirectToPage("/Lessons/Index");
    }
}