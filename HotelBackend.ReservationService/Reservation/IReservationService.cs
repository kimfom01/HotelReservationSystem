namespace HotelBackend.ReservationService.Reservation;

public interface IReservationService
{
    Task<ReservationDto> MakeReservation(ReservationDto reservationDto);
    Task<ReservationModel?> GetReservation(Guid id);
    Task<IEnumerable<ReservationModel>?> GetReservations();
}
