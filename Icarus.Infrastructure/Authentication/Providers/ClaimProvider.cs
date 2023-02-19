using Icarus.Application.Abstractions.Data;
using Icarus.Domain.Entities;
using Icarus.Domain.Enums;
using Icarus.Infrastructure.Authentication.Abstractions;
using Icarus.Infrastructure.Authentication.Constants;
using Icarus.Infrastructure.Authentication.Permissions;
using System.Security.Claims;

namespace Icarus.Infrastructure.Authentication.Providers;

public class ClaimProvider : IClaimsProvider
{
    private readonly IDbContext _dbContext;

    public ClaimProvider(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Claim[]> GetClaimsAsync(Member member)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtClaimTypes.MemberId, member.Id.ToString()),
            new Claim(JwtClaimTypes.Email, member.Email.ToString())
        };

        var permissionCalculator = new PermissionCalculator(_dbContext);

        PermissionEnum[] permissions = await PermissionCalculator.CalculatePermissionsForMemberAsync(member.Id);

        IEnumerable<Claim> permissionClaims = permissions
            .Select(p => new Claim(JwtClaimTypes.Permissions, p.ToString()));

        claims.AddRange(permissionClaims);

        return claims.ToArray();
    }
}
