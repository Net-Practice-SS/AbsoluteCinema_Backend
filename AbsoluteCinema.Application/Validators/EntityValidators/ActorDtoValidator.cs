using AbsoluteCinema.Application.DTO.Entities;
using FluentValidation;

namespace AbsoluteCinema.Application.Validators.EntityValidators;

public class ActorDtoValidator : AbstractValidator<ActorDto>
{
    public ActorDtoValidator()
    {
        RuleFor(a => a.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .MaximumLength(50).WithMessage("FirstName cannot exceed 50 characters.");

        RuleFor(a => a.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .MaximumLength(50).WithMessage("LastName cannot exceed 50 characters.");
    }
}