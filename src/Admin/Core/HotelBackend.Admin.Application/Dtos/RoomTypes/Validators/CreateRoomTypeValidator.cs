using FluentValidation;

namespace HotelBackend.Admin.Application.Dtos.RoomTypes.Validators;

public class CreateRoomTypeValidator : AbstractValidator<CreateRoomTypeDto>
{
    public CreateRoomTypeValidator()
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