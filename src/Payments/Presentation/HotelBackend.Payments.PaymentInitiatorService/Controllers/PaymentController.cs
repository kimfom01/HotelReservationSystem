using HotelBackend.Payments.Application.Contracts.Infrastructure;
using HotelBackend.Payments.Application.Dtos.Reservations;
using HotelBackend.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Payments.PaymentInitiatorService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentStatusPublisher _paymentStatusPublisher;

    public PaymentController(IPaymentStatusPublisher paymentStatusPublisher)
    {
        _paymentStatusPublisher = paymentStatusPublisher;
    }

    [HttpGet]
    public IActionResult HelloPayment()
    {
        return Ok("Hello from payment controller");
    }

    [HttpPost("pay/{reservationId:Guid}")]
    public async Task<IActionResult> PayForReservation(Guid reservationId)
    {
        var statuses = Enum.GetValues(typeof(PaymentStatus));

        var rand = new Random();

        var status = (PaymentStatus)(statuses.GetValue(rand.Next(statuses.Length)) ?? PaymentStatus.PENDING);

        await _paymentStatusPublisher.PublishMessage(new UpdateReservationPaymentStatusDto
        {
            Status = status,
            ReservationId = reservationId
        });

        return Ok("Payment initiated");
    }
}