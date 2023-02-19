using Icarus.Domain.Entities;
using Icarus.Domain.Enums;
using Icarus.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Icarus.Persistence.DataTableConfigurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);

        builder.HasKey(p => p.Id);

        builder.HasData(GetPermissions());
    }

    private static IEnumerable<Permission> GetPermissions()
    {
        IEnumerable<Permission> permissions = Enum
            .GetValues<PermissionEnum>()
            .Select(p => new Permission
            {
                Id = (int)p,
                Name = p.ToString()
            });

        return permissions;
    }
}
