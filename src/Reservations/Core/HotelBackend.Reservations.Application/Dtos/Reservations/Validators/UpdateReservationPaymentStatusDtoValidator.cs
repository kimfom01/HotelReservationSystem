using FluentValidation;
using HotelBackend.Reservations.Application.Contracts.Persistence;

namespace HotelBackend.Reservations.Application.Dtos.Reservations.Validators;

public class UpdateReservationPaymentStatusDtoValidator : AbstractValidator<UpdateReservationPaymentStatusDto>
{
    public UpdateReservationPaymentStatusDtoValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(dto => dto.Status)
            .IsInEnum()
            .WithMessage("{PropertyName} is not a valid value");

        RuleFor(dto => dto.ReservationId)
            .MustAsync(async (reservationId, token) =>
            {
                var reservation = await unitOfWork.Reservations.GetEntity(res => res.Id == reservationId, token);

                return reservation is not null;
            })
            .WithMessage("{PropertyName} does not exist");
    }
}