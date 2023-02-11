namespace Icarus.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    List<IDomainEvent> _domainEvents = new();
    protected AggregateRoot(Guid id) : base(id)
    {
    }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public void ClearDomainEvents() => _domainEvents.Clear();
    protected void RaiseDomainEvent(IDomainEvent _event)
    {
        _domainEvents.Add(_event);
    }
}
