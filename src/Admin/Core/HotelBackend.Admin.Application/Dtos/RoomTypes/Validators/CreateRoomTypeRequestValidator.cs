using FluentValidation;

namespace HotelBackend.Admin.Application.Dtos.RoomTypes.Validators;

public class CreateRoomTypeRequestValidator : AbstractValidator<CreateRoomTypeRequest>
{
    public CreateRoomTypeRequestValidator()
    {
        RuleFor(rt => rt.Type)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(rt => rt.Capacity)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}");
        
        RuleFor(rt => rt.Description)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(rt => rt.RoomPrice)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}");
    }
}