using AutoMapper;
using HotelBackend.ReservationService.Data;

namespace HotelBackend.ReservationService.Hotel;

public class HotelService : IHotelService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<HotelService> _logger;
    private readonly IMapper _mapper;

    public HotelService(
        IUnitOfWork unitOfWork, 
        ILogger<HotelService> logger,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<HotelDto>?> GetHotels()
    {
        var hotels = await _unitOfWork.Hotels.GetEntities(ho => true);

        return _mapper.Map<IEnumerable<HotelDto>>(hotels);
    }
}