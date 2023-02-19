using Icarus.Application.Abstractions.Data;
using Icarus.Application.Abstractions.Messaging;
using Icarus.Domain.Entities;
using Icarus.Domain.Errors;
using Icarus.Domain.Repositories;
using Icarus.Domain.Shared;
using Icarus.Domain.ValueObjects;
using Icarus.Domain.Extensions;
using Icarus.Application.Abstractions.Cryptography;

namespace Icarus.Application.Members.RegisterMember;

internal sealed class RegisterMemberCommandHandler
    : ICommandHandler<RegisterMemberCommand, Result<Guid>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<Guid>> Handle(RegisterMemberCommand request, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.Email);
        Result<Password> passwordResult = Password.Create(request.Password);
        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);

        if (!await _memberRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
        {
            return Result.Failure<Guid>(DomainError.ErrorAuthentication.DuplicateEmail);
        }

        string hashPassword = _passwordHasher.HashPassword(passwordResult.Value);

        Result<Member> memberResult = Result
            .FirstFailureOrSuccess(emailResult, passwordResult, firstNameResult, lastNameResult)
            .Map(() => Result.Success(Member.Create(Guid.NewGuid(), emailResult.Value.Value, hashPassword, firstNameResult.Value.Value, lastNameResult.Value.Value)));

        _memberRepository.Add(memberResult.Value);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Success(memberResult.Value.Id);
    }
}
