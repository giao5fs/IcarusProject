using Icarus.Application.Members.Commands.Login;
using Icarus.Domain.Entities;

namespace Icarus.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    Task<TokenResponse> GenerateTokenAsync(Member member);
}
