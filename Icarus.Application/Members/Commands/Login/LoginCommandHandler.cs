using Icarus.Application.Abstractions.Authentication;
using Icarus.Application.Abstractions.Messaging;
using Icarus.Application.Members.Commands.Login;
using Icarus.Domain.Entity;
using Icarus.Domain.Errors;
using Icarus.Domain.Repositories;
using Icarus.Domain.Services;
using Icarus.Domain.Shared;
using Icarus.Domain.ValueObjects;
using MediatR;

namespace Icarus.Application.Members.Login;

internal sealed class LoginCommandHandler
    : ICommandHandler<LoginCommand, Result<TokenResponse>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHashChecker _passwordHashChecker;
    public LoginCommandHandler(IMemberRepository memberRepository, IJwtProvider jwtProvider, IPasswordHashChecker passwordHashChecker)
    {
        _memberRepository = memberRepository;
        _jwtProvider = jwtProvider;
        _passwordHashChecker = passwordHashChecker;
    }

    public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //Get member
        Result<Email> email = Email.Create(request.Email);

        Member? member = await _memberRepository.GetByEmailAsync(email.Value);

        if (member is null)
        {
            return Result.Failure<TokenResponse>(DomainError.ErrorAuthentication.InvalidEmailOrPassword);
        }

        var isPasswordMatching = member.VerifyPasswordHash(request.Password, _passwordHashChecker);

        if (!isPasswordMatching)
        {
            return Result.Failure<TokenResponse>(DomainError.ErrorAuthentication.InvalidEmailOrPassword);
        }

        //Generate JWT

        var token = _jwtProvider.GenerateTokenAsync(member);

        //Return JWT
        return Result.Success(token.Result);
    }
}
