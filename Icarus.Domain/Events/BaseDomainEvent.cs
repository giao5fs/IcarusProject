namespace Icarus.Domain.Events;

/// <summary>
/// Represents the base class all domain events inherit from.
/// </summary>
public abstract class BaseDomainEvent : IDomainEvent
{
    public Guid EventId { get; }

    public DateTime OccurredOnUtc { get; }

    protected internal BaseDomainEvent()
    {
        EventId = Guid.NewGuid();
        OccurredOnUtc = DateTime.UtcNow;
    }
}
