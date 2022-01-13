using FluentValidation;
using UniversityProject.Domain.Dto.User;

namespace UniversityProject.Domain.FluentValidation;

public class LoginDtoValidator: AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}