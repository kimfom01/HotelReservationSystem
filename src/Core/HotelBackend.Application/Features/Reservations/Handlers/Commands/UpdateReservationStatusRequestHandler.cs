using FluentValidation;
using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Application.Dtos.Reservations;
using HotelBackend.Application.Dtos.Reservations.Validators;
using HotelBackend.Application.Exceptions;
using HotelBackend.Application.Features.Reservations.Requests.Commands;
using HotelBackend.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Application.Features.Reservations.Handlers.Commands;

public class UpdateReservationStatusRequestHandler : IRequestHandler<UpdateReservationStatusRequest, Unit>
{
    private readonly ILogger<UpdateReservationStatusRequestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateReservationStatusRequestHandler(
        ILogger<UpdateReservationStatusRequestHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateReservationStatusRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating reservation status");
        var validator = new UpdateReservationPaymentStatusDtoValidator(_unitOfWork);

        if (request.UpdateReservationPaymentStatusDto is null)
        {
            throw new ArgumentNullException(nameof(request), $"{nameof(UpdateReservationPaymentStatusDto)} is null");
        }

        var validationResult =
            await validator.ValidateAsync(request.UpdateReservationPaymentStatusDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var reservation =
            await _unitOfWork.Reservations.GetEntity(request.UpdateReservationPaymentStatusDto.ReservationId,
                cancellationToken);

        if (reservation is null)
        {
            throw new NotFoundException(
                $"Reservation with id={request.UpdateReservationPaymentStatusDto.ReservationId} could not be found");
        }

        reservation.PaymentStatus = request.UpdateReservationPaymentStatusDto.Status;

        if (request.UpdateReservationPaymentStatusDto.Status == PaymentStatus.PAID)
        {
            reservation.ReservationStatus = ReservationStatus.CONFIRMED;
        }

        if (request.UpdateReservationPaymentStatusDto.Status == PaymentStatus.CANCELED ||
            request.UpdateReservationPaymentStatusDto.Status == PaymentStatus.REFUNDED)
        {
            reservation.ReservationStatus = ReservationStatus.CANCELLED;
        }

        await _unitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Successfully updated reservation");

        return Unit.Value;
    }
}