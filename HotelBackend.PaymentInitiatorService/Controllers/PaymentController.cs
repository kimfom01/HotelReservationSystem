using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Application.Models;
using HotelBackend.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.PaymentInitiatorService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IQueueService _queueService;

    public PaymentController(IQueueService queueService)
    {
        _queueService = queueService;
    }
    
    [HttpGet]
    public IActionResult HelloPayment()
    {
        return Ok("Hello from payment controller");
    }

    [HttpPost("pay/{reservationId:Guid}")]
    public async Task<IActionResult> PayForReservation(Guid reservationId)
    {
        await _queueService.PublishMessage(new PaymentStatus
        {
            Status = PaymentStatusEnum.PAID,
            ReservationId = reservationId
        });
        
        return Ok("Successfully paid");
    }
}