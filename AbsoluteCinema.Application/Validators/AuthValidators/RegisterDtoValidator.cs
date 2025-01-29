using AbsoluteCinema.Application.DTO.AuthDTO;
using FluentValidation;

namespace AbsoluteCinema.Application.Validators.AuthValidators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Length(2, 50).WithMessage("First name must be between 2-50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(2, 50).WithMessage("Last name must be between 2-50 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address format");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required")
            .LessThanOrEqualTo(DateTime.Now.AddYears(-16)).WithMessage("Minimum age is 16 years");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match");
    }
}