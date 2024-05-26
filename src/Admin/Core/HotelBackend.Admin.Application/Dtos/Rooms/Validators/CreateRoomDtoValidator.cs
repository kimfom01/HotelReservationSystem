using FluentValidation;

namespace HotelBackend.Admin.Application.Dtos.Rooms.Validators;

public class CreateRoomDtoValidator : AbstractValidator<CreateRoomDto>
{
    public CreateRoomDtoValidator()
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