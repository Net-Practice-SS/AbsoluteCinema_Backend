using AbsoluteCinema.Application.DTO.Entities;
using FluentValidation;

namespace AbsoluteCinema.Application.Validators.EntityValidators;

public class HallDtoValidator : AbstractValidator<HallDto>
{
    public HallDtoValidator()
    {
        RuleFor(h => h.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(h => h.RowCount)
            .NotNull().WithMessage("RowCount cannot be null")
            .GreaterThanOrEqualTo(1).WithMessage("RowCount should be >= 1.");

        RuleFor(h => h.PlaceCount)
            .NotNull().WithMessage("PlaceCount cannot be null")
            .GreaterThanOrEqualTo(1).WithMessage("PlaceCount should be >= 1.");
    }
}