using FluentValidation;

namespace Admin.Application.Dtos.Admin.Rooms.Validators;

public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
{
    public CreateRoomRequestValidator()
    {
        RuleFor(ro => ro.RoomNumber)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(ro => ro.HotelId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(ro => ro.RoomTypeId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");
    }
}