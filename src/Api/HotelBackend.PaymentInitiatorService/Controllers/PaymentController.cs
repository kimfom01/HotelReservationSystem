using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Application.Dtos.Reservations;
using HotelBackend.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.PaymentInitiatorService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IReservationQueueService _reservationQueueService;

    public PaymentController(IReservationQueueService reservationQueueService)
    {
        _reservationQueueService = reservationQueueService;
    }
    
    [HttpGet]
    public IActionResult HelloPayment()
    {
        return Ok("Hello from payment controller");
    }

    [HttpPost("pay/{reservationId:Guid}")]
    public async Task<IActionResult> PayForReservation(Guid reservationId)
    {
        await _reservationQueueService.PublishMessage(new UpdateReservationPaymentStatusDto
        {
            Status = PaymentStatus.PAID,
            ReservationId = reservationId
        });
        
        return Ok("Successfully paid");
    }
}