using AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO;
using FluentValidation;

namespace AbsoluteCinema.Application.Validators.EntityValidators;

public class SessionDtoValidator : AbstractValidator<SessionDto>
{
    public SessionDtoValidator()
    {
        RuleFor(s => s.MovieId)
            .GreaterThan(0).WithMessage("MovieId must be greater than 0.");

        RuleFor(s => s.HallId)
            .GreaterThan(0).WithMessage("HallId must be greater than 0.");

        RuleFor(s => s.Date)
            .GreaterThan(DateTime.UtcNow.AddMinutes(-1))
            .WithMessage("Session Date must be in the present or future.");
    }
}