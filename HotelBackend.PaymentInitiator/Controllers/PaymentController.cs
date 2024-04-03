using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.PaymentInitiator.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PaymentController : ControllerBase
{
    [HttpGet]
    public IActionResult HelloPayment()
    {
        return Ok("Hello from payment controller");
    }
}