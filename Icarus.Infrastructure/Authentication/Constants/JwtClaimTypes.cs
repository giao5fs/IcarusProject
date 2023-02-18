using Icarus.Infrastructure.Authentication.Permissions;

namespace Icarus.Infrastructure.Authentication.Constants;

internal static class JwtClaimTypes
{
    internal const string MemberId = "memberId";
    internal const string Email = "email";
    internal const string Name = "name";
    internal const string Permissions = PermissionConstants.PermissionsClaimType;
}
