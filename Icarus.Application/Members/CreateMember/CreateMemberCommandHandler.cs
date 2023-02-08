using Icarus.Application.Abstractions.Messaging;
using Icarus.Domain.Entity;
using Icarus.Domain.Errors;
using Icarus.Domain.Repositories;
using Icarus.Domain.Shared;

namespace Icarus.Application.Members.CreateMember
{
    internal sealed class CreateMemberCommandHandler
        : ICommandHandler<CreateMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            if(!await _memberRepository.IsEmailUniqueAsync(request.Email, cancellationToken))
            {
                return Result.Failure<Guid>(DomainError.Member.EmailAlreadyInUse);
            }

            var member = Member.Create(
                Guid.NewGuid(),
                request.Email,
                request.FirstName,
                request.LastName);

            _memberRepository.Add(member);

            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return member.Id;
        }
    }
}
