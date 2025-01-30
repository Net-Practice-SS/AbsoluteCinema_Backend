using AbsoluteCinema.Application.DTO.Entities;
using FluentValidation;

namespace AbsoluteCinema.Application.Validators.EntityValidators;

public class MovieDtoValidator : AbstractValidator<MovieDto>
{
    public MovieDtoValidator()
    {
        RuleFor(m => m.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");
        
        RuleFor(m => m.Score)
            .InclusiveBetween(0, 10)
            .When(m => m.Score.HasValue)
            .WithMessage("Score must be between 0 and 10.");
        
        RuleFor(m => m.ReleaseDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .When(m => m.ReleaseDate.HasValue)
            .WithMessage("Release date cannot be in the future.");
        
        RuleFor(m => m.Discription)
            .MaximumLength(1000)
            .WithMessage("Description cannot exceed 1000 characters.");
        
        RuleFor(m => m.PosterPath)
            .MaximumLength(500)
            .WithMessage("PosterPath cannot exceed 500 characters.");
    }
}