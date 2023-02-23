using Icarus.Domain.DomainEvents;
using Icarus.Domain.Events;

namespace Icarus.Application.Members.Events;
public sealed class PerformBackgroundCheckWhenMemberRegisteredDomainEventHandler
    : IDomainEventHandler<MemberRegisteredDomainEvent>
{
    public Task Handle(MemberRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
