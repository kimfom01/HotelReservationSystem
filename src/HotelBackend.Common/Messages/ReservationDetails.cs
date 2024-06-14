using HotelBackend.Common.Enums;

namespace HotelBackend.Common.Messages;

public record ReservationDetails
{
    public Guid Id { get; init; }
    public DateTime CreationDate { get; init; }
    public DateTime CheckIn { get; init; }
    public DateTime CheckOut { get; init; }
    public ReservationStatus ReservationStatus { get; init; }
    public PaymentStatus PaymentStatus { get; init; }
    public string? PaymentId { get; init; }
    public string? SpecialRequests { get; init; }
    public string? RoomPreferences { get; init; }
    public int NumberOfGuests { get; init; }
    public Guid GuestProfileId { get; init; }
    public string GuestFirstName { get; init; } = string.Empty;
    public string GuestLastName { get; init; } = string.Empty;
    public string GuestContactEmail { get; init; } = string.Empty;
    public string GuestFullName => $"{GuestFirstName} {GuestLastName}";
    public Guid HotelId { get; init; }
    public string HotelName { get; init; } = string.Empty;
    public string HotelLocation { get; init; } = string.Empty;
    public Guid RoomId { get; init; }
    public string RoomNumber { get; init; } = string.Empty;
    public bool RoomAvailability { get; init; }
    public Guid RoomTypeId { get; init; }
    public string RoomType { get; init; } = string.Empty;
    public int RoomCapacity { get; init; }
    public string RoomDescription { get; init; } = string.Empty;
    public decimal RoomPriceValue { get; init; }
}