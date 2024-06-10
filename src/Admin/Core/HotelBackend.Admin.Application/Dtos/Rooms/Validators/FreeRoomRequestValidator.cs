using FluentValidation;

namespace HotelBackend.Admin.Application.Dtos.Rooms.Validators;

public class FreeRoomRequestValidator : AbstractValidator<FreeRoomRequest>
{
    public FreeRoomRequestValidator()
    {
        RuleFor(rom => rom.RoomId)
            .NotNull()
            .WithMessage("{PropertyName} is required.");

        RuleFor(rom => rom.HotelId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
    }
}