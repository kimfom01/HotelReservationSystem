namespace Hrs.Common.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
}