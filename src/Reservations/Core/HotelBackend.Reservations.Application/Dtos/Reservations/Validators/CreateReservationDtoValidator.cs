using FluentValidation;
using HotelBackend.Reservations.Application.Contracts.Persistence;
using HotelBackend.Reservations.Application.Dtos.GuestProfiles.Validators;

namespace HotelBackend.Reservations.Application.Dtos.Reservations.Validators;

public class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationDtoValidator(IUnitOfWork unitOfWork)
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

        RuleFor(dto => dto.HotelId)
            .NotNull()
            .MustAsync(async (hotelId, token) =>
            {
                var hotel = await unitOfWork.Hotels.GetEntity(hot => hot.Id == hotelId, token);

                return hotel is not null;
            })
            .WithMessage("{PropertyName} does not exist");

        RuleFor(dto => dto.RoomId)
            .NotNull()
            .MustAsync(async (roomId, token) =>
            {
                var room = await unitOfWork.Rooms.GetEntity(hot => hot.Id == roomId, token);

                return room is not null;
            })
            .WithMessage("{PropertyName} does not exist");
    }
}