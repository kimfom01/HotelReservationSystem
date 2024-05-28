using FluentValidation;
using HotelBackend.Reservations.Application.Dtos.GuestProfiles.Validators;

namespace HotelBackend.Reservations.Application.Dtos.Reservations.Validators;

public class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationDtoValidator()
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
            .SetValidator(new GuestProfileValidator()!);
    }
}