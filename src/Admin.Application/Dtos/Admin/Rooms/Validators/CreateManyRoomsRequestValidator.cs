using FluentValidation;

namespace Admin.Application.Dtos.Admin.Rooms.Validators;

public class CreateManyRoomsRequestValidator: AbstractValidator<CreateManyRoomsRequest>
{
    public CreateManyRoomsRequestValidator()
    {
        RuleFor(r => r.HotelId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");
        
        RuleFor(r => r.RoomTypeId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");
        
        RuleFor(r => r.Count)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .GreaterThanOrEqualTo(1)
            .WithMessage("{PropertyName} must be {ComparisonValue} or more");
        
        RuleFor(r => r.Start)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .GreaterThanOrEqualTo(1)
            .WithMessage("{PropertyName} must be {ComparisonValue} or more");
    }
}