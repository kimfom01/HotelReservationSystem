using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class Reservation
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("checkIn")]
    public DateTime CheckIn { get; set; }

    [JsonPropertyName("checkOut")]
    public DateTime CheckOut { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("tax")]
    public decimal Tax { get; set; }

    [JsonPropertyName("discount")]
    public double Discount { get; set; }

    [JsonPropertyName("guestId")]
    public int GuestId { get; set; }

    [JsonPropertyName("hotelId")]
    public int HotelId { get; set; }

    [JsonPropertyName("numberOfGuests")]
    public int NumberOfGuests { get; set; }
}
