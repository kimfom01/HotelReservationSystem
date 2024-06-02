using FluentValidation;

namespace HotelBackend.Admin.Application.Dtos.Rooms.Validators;

public class ReserveRoomRequestDtoValidator : AbstractValidator<ReserveRoomRequestDto>
{
    public ReserveRoomRequestDtoValidator()
    {
        RuleFor(rom => rom.RoomTypeId)
            .NotNull()
            .WithMessage("{PropertyName} is required.");

        RuleFor(rom => rom.HotelId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
    }
}