using AbsoluteCinema.Application.DTO.Entities;
using FluentValidation;

namespace AbsoluteCinema.Application.Validators.EntityValidators;

public class TicketStatusDtoValidator : AbstractValidator<TicketStatusDto>
{
    public TicketStatusDtoValidator()
    {
        RuleFor(ts => ts.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");
    }
}