namespace Icarus.Domain.Events;

/// <summary>
/// Represents the base class all domain events inherit from.
/// </summary>
public abstract record BaseDomainEvent(Guid eventId) : IDomainEvent
{
    public Guid EventId => eventId;

    public DateTime OccurredOnUtc => DateTime.UtcNow;
}
