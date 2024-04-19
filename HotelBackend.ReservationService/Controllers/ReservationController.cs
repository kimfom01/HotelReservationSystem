using AutoMapper;
using HotelBackend.Application.Contracts.Features;
using HotelBackend.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.ReservationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IReservationService _reservationService;
    private readonly ILogger<ReservationController> _logger;

    public ReservationController(
        IMapper mapper,
        IReservationService reservationService, ILogger<ReservationController> logger)
    {
        _mapper = mapper;
        _reservationService = reservationService;
        _logger = logger;
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReservation(Guid id, CancellationToken cancellationToken)
    {
        var reservation = await _reservationService.GetReservation(id, cancellationToken);

        if (reservation is null)
        {
            return NotFound();
        }

        var reservationDto = _mapper.Map<ReservationDto>(reservation);

        return Ok(reservationDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetReservations(CancellationToken cancellationToken)
    {
        var reservations = await _reservationService.GetReservations(cancellationToken);

        var reservationsDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

        return Ok(reservationsDtos);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostReservation(ReservationDto reservationDto, CancellationToken cancellationToken)
    {
        try
        {
            var reservation = _mapper.Map<Domain.Entities.Reservation>(reservationDto);

            var added = await _reservationService.MakeReservation(reservation, cancellationToken);

            _logger.LogInformation("Successfully made reservation with id={reservationId}", added.Id);
            return CreatedAtAction(nameof(GetReservation), new { id = added.Id }, added);
        }
        catch (Exception exception)
        {
            _logger.LogError("Unable to make reservation: {exceptionMessage}", exception.Message);
            return Conflict($"Unable to make reservation: {exception.Message}");
        }
    }
}