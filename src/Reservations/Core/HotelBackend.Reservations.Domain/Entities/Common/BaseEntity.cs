namespace HotelBackend.Reservations.Domain.Entities.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? LastModifiedAt { get; set; }
}