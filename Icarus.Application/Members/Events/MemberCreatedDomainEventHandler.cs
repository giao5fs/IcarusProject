using Icarus.Application.Abstractions;
using Icarus.Domain.DomainEvents;
using Icarus.Domain.Repositories;
using MediatR;

namespace Icarus.Application.Members.Events
{
    internal class MemberCreatedDomainEventHandler :
        INotificationHandler<MemberCreatedDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IMemberRepository _memberRepository;
        public MemberCreatedDomainEventHandler(IEmailService emailService, IMemberRepository memberRepository)
        {
            _emailService = emailService;
            _memberRepository = memberRepository;
        }

        public async Task Handle(MemberCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(notification.id, cancellationToken);

            if (member is null)
            {
                return;
            }

            await _emailService.SendCreatedMemberAsync(member, cancellationToken);
        }
    }
}
