using FluentValidation;
using Hrs.Application.Contracts.Database;

namespace Hrs.Application.Dtos.Reservations.Validators;

public class UpdateReservationPaymentStatusRequestValidator : AbstractValidator<UpdateReservationPaymentStatusRequest>
{
    public UpdateReservationPaymentStatusRequestValidator(IReservationsUnitOfWork reservationsUnitOfWork)
    {
        RuleFor(dto => dto.Status)
            .IsInEnum()
            .WithMessage("{PropertyName} is not a valid value");

        RuleFor(dto => dto.ReservationId)
            .MustAsync(async (reservationId, token) =>
            {
                var reservation = await reservationsUnitOfWork.Reservations.GetEntity(res => res.Id == reservationId, token);

                return reservation is not null;
            })
            .WithMessage("{PropertyName} does not exist");
        
        RuleFor(dto => dto.PaymentId)
            .NotNull()
            .WithMessage("{PropertyName} is required.");
    }
}