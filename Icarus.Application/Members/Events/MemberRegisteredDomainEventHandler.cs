using Icarus.Application.Abstractions;
using Icarus.Domain.DomainEvents;
using Icarus.Domain.Repositories;
using MediatR;

namespace Icarus.Application.Members.Events;
public sealed class MemberRegisteredDomainEventHandler :
    INotificationHandler<MemberRegisteredDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IMemberRepository _memberRepository;
    public MemberRegisteredDomainEventHandler(IEmailService emailService, IMemberRepository memberRepository)
    {
        _emailService = emailService;
        _memberRepository = memberRepository;
    }
       
    public async Task Handle(MemberRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(notification.MemberId, cancellationToken);

        if (member is null)
        {
            return;
        }

        await _emailService.SendRegisteredMemberAsync(member, cancellationToken);
    }
}
