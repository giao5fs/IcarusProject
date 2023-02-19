using Icarus.Domain.Entities;
using System.Security.Claims;

namespace Icarus.Infrastructure.Authentication.Abstractions;

public interface IClaimsProvider
{
    Task<Claim[]> GetClaimsAsync(Member member);
}
