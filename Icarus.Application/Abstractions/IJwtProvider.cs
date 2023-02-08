using Icarus.Domain.Entity;

namespace Icarus.Application.Abstractions;

public interface IJwtProvider
{
    string GenerateToken(Member member);
}
