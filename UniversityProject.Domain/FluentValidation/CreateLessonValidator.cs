using FluentValidation;
using UniversityProject.Domain.Dto.Lessons;

namespace UniversityProject.Domain.FluentValidation;

public class CreateLessonValidator:AbstractValidator<CreateLessonDto>
{
    public CreateLessonValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}