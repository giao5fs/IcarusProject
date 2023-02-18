using Icarus.Application.Abstractions.Authentication;
using Icarus.Infrastructure.Authentication.Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Icarus.Infrastructure.Authentication.Providers;

public sealed class MemberIdentifierProvider : IMemberIdentifierProvider
{
    public Guid MemberId { get; }
    public MemberIdentifierProvider(IHttpContextAccessor httpContext)
    {
        string userIdClaim = httpContext.HttpContext?.User?.FindFirstValue(
            JwtClaimTypes.MemberId) ?? throw new ArgumentException("The user identifier claim is required.", nameof(httpContext));

        MemberId = new Guid(userIdClaim);
    }
}
