using Icarus.Domain.Entities;
using Icarus.Infrastructure.Authentication.Abstractions;
using Icarus.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Icarus.Infrastructure.Authentication.Permissions;
public class PermissionService : IPermissionService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(Guid memberId)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var _context = scope.ServiceProvider.GetService<IcarusDbContext>();

        ICollection<Role>[] roles = await _context!.Set<Member>()
        .Include(x => x.Roles)
        .ThenInclude(x => x.Permissions)
        .Where(x => x.Id == memberId)
        .Select(x => x.Roles)
        .ToArrayAsync();

        return roles
            .SelectMany(x => x)
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
