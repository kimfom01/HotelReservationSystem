using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos;
using HotelBackend.Admin.Application.Features.Hotels.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Hotels.Handlers.Queries;

public class GetHotelListRequestHandler : IRequestHandler<GetHotelListRequest, List<HotelDto>>
{
    private readonly ILogger<GetHotelListRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetHotelListRequestHandler(
        ILogger<GetHotelListRequestHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<HotelDto>> Handle(GetHotelListRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all hotels");
        var hotels = await _unitOfWork.Hotels.GetEntities(hot => true, cancellationToken);

        if (hotels.Count == 0)
        {
            _logger.LogWarning("There are no hotels found or registered!");
            return [];
        }

        return _mapper.Map<List<HotelDto>>(hotels);
    }
}