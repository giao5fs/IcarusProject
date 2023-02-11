using Icarus.Domain.Primitives;

namespace Icarus.Domain.DomainEvents;

public sealed record MemberRegisteredDomainEvent(Guid memberId) : IDomainEvent
{
}
