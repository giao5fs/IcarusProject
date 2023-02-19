using Icarus.Application.Abstractions.Data;
using Icarus.Domain.Enums;

namespace Icarus.Infrastructure.Authentication.Permissions;

internal sealed class PermissionCalculator
{
    private readonly IDbContext _dbContext;

    public PermissionCalculator(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    internal static async Task<PermissionEnum[]> CalculatePermissionsForMemberAsync(Guid guid)
    {
        await Task.Delay(1);
        return new[]
        {
            PermissionEnum.AccessEverything
        };
    }
}
