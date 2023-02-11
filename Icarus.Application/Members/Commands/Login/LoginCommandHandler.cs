using Icarus.Application.Abstractions;
using Icarus.Application.Abstractions.Messaging;
using Icarus.Domain.Entity;
using Icarus.Domain.Errors;
using Icarus.Domain.Repositories;
using Icarus.Domain.Shared;
using Icarus.Domain.ValueObjects;

namespace Icarus.Application.Members.Login;

internal sealed class LoginCommandHandler
    : ICommandHandler<LoginCommand, string>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IJwtProvider _jwtProvider;
    public LoginCommandHandler(IMemberRepository memberRepository, IJwtProvider jwtProvider)
    {
        _memberRepository = memberRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //Get member
        Result<Email> email = Email.Create(request.Email);

        Member? member = await _memberRepository.GetByEmailAsync(email.Value);

        if (member is null)
        {
            return Result.Failure<string>(DomainError.Member.InvalidCredentials);
        }

        //Generate JWT

        var token = _jwtProvider.GenerateToken(member);

        //Return JWT
        return token;
    }
}
