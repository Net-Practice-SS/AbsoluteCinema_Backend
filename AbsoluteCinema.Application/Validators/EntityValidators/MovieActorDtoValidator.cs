using AbsoluteCinema.Application.DTO.Entities;
using FluentValidation;

namespace AbsoluteCinema.Application.Validators.EntityValidators;

public class MovieActorDtoValidator : AbstractValidator<MovieActorDto>
{
    public MovieActorDtoValidator()
    {
        RuleFor(ma => ma.MovieId)
            .GreaterThan(0).WithMessage("MovieId must be greater than 0.");

        RuleFor(ma => ma.ActorId)
            .GreaterThan(0).WithMessage("ActorId must be greater than 0.");

        RuleFor(ma => ma.CharacterName)
            .NotEmpty().WithMessage("CharacterName is required.");
    }
}