using Icarus.Infrastructure.Authentication.Abstractions;
using Icarus.Infrastructure.Authentication.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Icarus.Infrastructure.Authentication.Permissions;

public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionService _permissionService;
    public PermissionAuthorizationHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var memberId = context.User.Claims.FirstOrDefault(
            x => x.Type == JwtClaimTypes.MemberId)?.Value;

        if (!Guid.TryParse(memberId, out Guid parsedMemberId))
        {
            return;
        }

        HashSet<string> permissions = await _permissionService.GetPermissionsAsync(parsedMemberId);
        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}
