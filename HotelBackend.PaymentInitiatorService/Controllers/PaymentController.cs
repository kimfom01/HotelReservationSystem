using HotelBackend.Infrastructure.Infrastructure;
using HotelBackend.Infrastructure.Statuses;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.PaymentInitiatorService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly QueueService<PaymentStatus> _queueService;

    public PaymentController(QueueService<PaymentStatus> queueService)
    {
        _queueService = queueService;
    }
    
    [HttpGet]
    public IActionResult HelloPayment()
    {
        return Ok("Hello from payment controller");
    }

    [HttpPost("pay/{reservationId:Guid}")]
    public async Task<IActionResult> PayForReservation()
    {
        await _queueService.PublishMessage(new PaymentStatus
        {
            Status = "Paid"
        });
        
        return Ok("Successfully paid");
    }
}