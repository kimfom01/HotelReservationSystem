using AutoMapper;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Dtos.Hotels;
using HotelBackend.Reservations.Application.Exceptions;
using HotelBackend.Reservations.Application.Features.Admin.Hotels.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Application.Features.Admin.Hotels.Handlers.Queries;

public class GetHotelQueryHandler : IRequestHandler<GetHotelQuery, GetHotelResponse>
{
    private readonly ILogger<GetHotelQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IAdminUnitOfWork _unitOfWork;

    public GetHotelQueryHandler(
        ILogger<GetHotelQueryHandler> logger,
        IMapper mapper,
        IAdminUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetHotelResponse> Handle(GetHotelQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting hotel with id={HotelId}", query.HotelId);
        var hotel = await _unitOfWork.Hotels.GetEntity(hot => hot.Id == query.HotelId, cancellationToken);

        if (hotel is null)
        {
            _logger.LogWarning("Hotel with id={HotelId} does not exist", query.HotelId);
            throw new NotFoundException($"Hotel with id={query.HotelId} does not exist");
        }

        return _mapper.Map<GetHotelResponse>(hotel);
    }
}