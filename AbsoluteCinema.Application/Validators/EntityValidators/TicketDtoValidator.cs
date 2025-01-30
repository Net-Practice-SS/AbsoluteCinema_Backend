using AbsoluteCinema.Application.DTO.Entities;
using FluentValidation;

namespace AbsoluteCinema.Application.Validators.EntityValidators;

public class TicketDtoValidator : AbstractValidator<TicketDto>
{
    public TicketDtoValidator()
    {
        RuleFor(t => t.SessionId)
            .GreaterThan(0).WithMessage("SessionId must be greater than 0.");

        RuleFor(t => t.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0.");

        RuleFor(t => t.Row)
            .GreaterThanOrEqualTo(1).WithMessage("Row must be >= 1.");

        RuleFor(t => t.Place)
            .GreaterThanOrEqualTo(1).WithMessage("Place must be >= 1.");

        RuleFor(t => t.StatusId)
            .GreaterThan(0).WithMessage("StatusId must be greater than 0.");
    }
}