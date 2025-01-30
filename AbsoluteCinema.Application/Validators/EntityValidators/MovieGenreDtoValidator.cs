using AbsoluteCinema.Application.DTO.Entities;
using FluentValidation;

namespace AbsoluteCinema.Application.Validators.EntityValidators;

public class MovieGenreDtoValidator : AbstractValidator<MovieGenreDto>
{
    public MovieGenreDtoValidator()
    {
        RuleFor(mg => mg.MovieId)
            .GreaterThan(0).WithMessage("MovieId must be greater than 0.");

        RuleFor(mg => mg.GenreId)
            .GreaterThan(0).WithMessage("GenreId must be greater than 0.");
    }

}