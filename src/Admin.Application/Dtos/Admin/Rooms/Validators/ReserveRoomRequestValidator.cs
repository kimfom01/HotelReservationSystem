using FluentValidation;

namespace Admin.Application.Dtos.Admin.Rooms.Validators;

public class ReserveRoomRequestValidator : AbstractValidator<ReserveRoomRequest>
{
    public ReserveRoomRequestValidator()
    {
        RuleFor(rom => rom.RoomTypeId)
            .NotNull()
            .WithMessage("{PropertyName} is required.");

        RuleFor(rom => rom.HotelId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
    }
}