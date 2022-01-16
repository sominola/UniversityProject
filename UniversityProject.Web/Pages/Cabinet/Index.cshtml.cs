using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityProject.Data.Repositories.Interfaces;
using UniversityProject.Domain.Dto.Lessons;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Web.Pages.Cabinet;

public class IndexModel : PageModel
{
    private readonly ILessonService _lessonService;
    [BindProperty]
    public LessonIndexDto LessonIndexDto { get; set; }

    public IndexModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }
    
    public async Task OnGetAsync()
    {
        LessonIndexDto = await _lessonService.GetLessonExcludeCurrentUser();
    }
}