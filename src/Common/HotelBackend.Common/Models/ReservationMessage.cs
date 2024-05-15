using HotelBackend.Common.Enums;

namespace HotelBackend.Common.Models;

public class ReservationMessage
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public ReservationStatus ReservationStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string? PaymentId { get; set; }
    public string? SpecialRequests { get; set; }
    public string? RoomPreferences { get; set; }
    public int NumberOfGuests { get; set; }
    public Guid GuestProfileId { get; set; }
    public string GuestFirstName { get; set; } = string.Empty;
    public string GuestLastName { get; set; } = string.Empty;
    public string GuestContactEmail { get; set; } = string.Empty;
    public string GuestFullName => $"{GuestFirstName} {GuestLastName}";
    public Guid HotelId { get; set; }
    public string HotelName { get; set; } = string.Empty;
    public string HotelLocation { get; set; } = string.Empty;
    public Guid RoomId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public bool RoomAvailability { get; set; }
    public Guid RoomTypeId { get; set; }
    public string RoomType { get; set; } = string.Empty;
    public int RoomCapacity { get; set; }
    public string RoomDescription { get; set; } = string.Empty;
    public decimal RoomPriceValue { get; set; }
}