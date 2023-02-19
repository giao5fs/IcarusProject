using Icarus.Domain.Entities;
using Icarus.Infrastructure.Authentication.Abstractions;
using Icarus.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Icarus.Infrastructure.Authentication.Permissions;
public class PermissionService : IPermissionService
{
    private readonly IcarusDbContext _context;

    public PermissionService(IcarusDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(Guid memberId)
    {
        var roles = await _context.Set<Member>()
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
