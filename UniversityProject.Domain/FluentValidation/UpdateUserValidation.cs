using FluentValidation;
using UniversityProject.Domain.Dto.User;

namespace UniversityProject.Domain.FluentValidation;

public class UpdateUserValidation:AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidation()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty();
        RuleFor(x => x.LastName).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
    }
}