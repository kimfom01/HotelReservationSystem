using FluentValidation;
using Hrs.Application.Dtos.GuestProfiles.Validators;

namespace Hrs.Application.Dtos.Reservations.Validators;

public class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator()
    {
        RuleFor(dto => dto.CheckIn)
            .LessThan(dto => dto.CheckOut)
            .WithMessage("{PropertyName} must be before {ComparisonValue}");

        RuleFor(dto => dto.CheckOut)
            .GreaterThan(cr => cr.CheckIn)
            .WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(dto => dto.NumberOfGuests)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}");

        RuleFor(dto => dto.GuestProfile)
            .NotNull()
            .SetValidator(new CreateGuestProfileRequestValidator()!);
    }
}