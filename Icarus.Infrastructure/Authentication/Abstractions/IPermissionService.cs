namespace Icarus.Infrastructure.Authentication.Abstractions;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(Guid memberId);
}
