using Icarus.Application.Abstractions.Messaging;
using Icarus.Domain.Entity;
using Icarus.Domain.Errors;
using Icarus.Domain.Repositories;
using Icarus.Domain.Shared;
using Icarus.Domain.ValueObjects;
using System.Diagnostics;
using static Icarus.Domain.Errors.DomainError;
using Member = Icarus.Domain.Entity.Member;

namespace Icarus.Application.Members.RegisterMember;

internal sealed class RegisterMemberCommandHandler
    : ICommandHandler<RegisterMemberCommand, Guid>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterMemberCommand request, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.Email);
        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);

        if (!await _memberRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
        {
            return Result.Failure<Guid>(DomainError.Member.EmailAlreadyInUse);
        }

        var member = Member.Create(
            Guid.NewGuid(),
            emailResult.Value,
            firstNameResult.Value,
            lastNameResult.Value);

        _memberRepository.Add(member);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return member.Id;

    }
}
