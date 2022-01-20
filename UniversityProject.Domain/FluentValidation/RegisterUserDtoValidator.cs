using FluentValidation;
using UniversityProject.Domain.Dto.User;

namespace UniversityProject.Domain.FluentValidation;

public class RegisterUserDtoValidator:AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty();
        RuleFor(x => x.LastName).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty()
            .MinimumLength(6).WithMessage("Password length must be at least 4 characters")
            .Equal(x => x.ConfirmPassword).WithMessage("Passwords are not the same");
    }
}