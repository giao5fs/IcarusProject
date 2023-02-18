using Icarus.Application.Abstractions.Data;
using Icarus.Application.Abstractions.Messaging;
using Icarus.Domain.Entity;
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

        var memberResult = CreateMemberResult(
            emailResult,
            passwordResult,
            _passwordHasher.HashPassword,
            firstNameResult,
            lastNameResult);

        _memberRepository.Add(memberResult.Value);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Success(memberResult.Value.Id);
    }

    private static Result<Member> CreateMemberResult(
        Result<Email> email,
        Result<Password> password,
        Func<Password, string> hashPassword,
        Result<FirstName> firstName,
        Result<LastName> lastName)
    {
        return Result
            .FirstFailureOrSuccess(firstName, lastName, email, password)
            .Map(() => Result.Success(
                Member.Create(
                    Guid.NewGuid(),
                    email.Value,
                    hashPassword(password.Value),
                    firstName.Value,
                    lastName.Value)));
    }
}
