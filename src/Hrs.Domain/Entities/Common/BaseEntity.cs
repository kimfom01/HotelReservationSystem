namespace Hrs.Domain.Entities.Common;

public abstract class BaseEntity
{
    public Guid Id { get; internal init; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
}