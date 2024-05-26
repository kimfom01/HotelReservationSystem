using FluentValidation;

namespace HotelBackend.Admin.Application.Dtos.Rooms.Validators;

public class UpdateRoomAvailabilityDtoValidator : AbstractValidator<UpdateRoomAvailabilityDto>
{
    public UpdateRoomAvailabilityDtoValidator()
    {
        RuleFor(rom => rom.Availability)
            .NotNull()
            .WithMessage("{PropertyName} is required.");

        RuleFor(rom => rom.HotelId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(rom => rom.RoomId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
    }
}